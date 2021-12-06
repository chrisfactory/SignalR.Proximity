using LazyCache;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks; 

namespace SignalR.Proximity.Hosting
{
    internal class DynamicMapHubFactoryMiddleware<THub> : IMiddleware
        where THub : Hub
    {
        private object _sync = new object();
        private IAppCache cache = new CachingService();
        private Func<RequestDelegate, RequestDelegate> _middleware = (r) => r;
        private IApplicationBuilder _appBuilder;
        private Action<IApplicationBuilder> _beforeMapHub = (i) => { };
        private string _path;
        private ConcurrentBag<Type> hubsBucket = new ConcurrentBag<Type>();
        private MethodInfo _MapHub;
        public DynamicMapHubFactoryMiddleware()
        {
            var codeCS = CreateDynamicHub();
            var proxyPattern = Build(codeCS).GetTypes();
            hubsBucket = new ConcurrentBag<Type>(proxyPattern);

            //public static HubEndpointConventionBuilder MapHub<THub>(this IEndpointRouteBuilder endpoints, string pattern) where THub : Hub;
            _MapHub = typeof(HubEndpointRouteBuilderExtensions).GetMethod("MapHub", new Type[] { typeof(IEndpointRouteBuilder), typeof(string) });

        }

        internal void Use(IApplicationBuilder app, string path, Action<IApplicationBuilder> beforeMapHub)
        {
            if (app == null)
                throw new ArgumentNullException(nameof(app));

            if (!string.IsNullOrEmpty(path))
                _path = path;
            else
                _path = "/";
            if (beforeMapHub != null)
                _beforeMapHub = beforeMapHub;
            _appBuilder = app.Use(next => context => _middleware(next)(context));
        }

        private void Configure(Action<IApplicationBuilder> action)
        {
            var app = _appBuilder.New();
            _beforeMapHub(app);
            action(app);
            _middleware = next => app.Use(_ => next).Build();
        }

        private static Dictionary<string, MethodInfo> maps = new Dictionary<string, MethodInfo>();
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            string keyValue = context.Request.Path;
            if (!string.IsNullOrEmpty(keyValue) && keyValue.StartsWith(_path) && keyValue.EndsWith("/negotiate"))
            {
                cache.GetOrAdd(keyValue, cacheEntry =>
                {
                    lock (_sync)
                    {
                        bool success = true;
                        cacheEntry.Priority = Microsoft.Extensions.Caching.Memory.CacheItemPriority.NeverRemove;
                        string patternMapRoute = keyValue.Remove((keyValue.Length - "/negotiate".Length), "/negotiate".Length);
                        Configure(runtimeApp =>
                        {
                            try
                            {
                                Type T1;
                                hubsBucket.TryTake(out T1);

                                MethodInfo generic = _MapHub.MakeGenericMethod(T1);
                                maps.Add(patternMapRoute, generic);
                                runtimeApp.UseEndpoints(endpoints =>
                                {
                                    foreach (var kv in maps)
                                    {
                                        kv.Value.Invoke(null, new object[] { endpoints, kv.Key });
                                    }
                                });
                            }
                            catch (Exception ex)
                            {
                                success = false;
                            }
                        });
                        return success;
                    }

                });

            }


            await next(context);
        }
        /// <summary>
        /// Create a class from scratch.
        /// </summary>
        static string CreateDynamicHub()
        {
            Type t = typeof(THub);
            // Create a namespace: (namespace)

            var @namespace = SyntaxFactory.NamespaceDeclaration(SyntaxFactory.ParseName(t.Namespace)).NormalizeWhitespace();
            // Add System using statement: (using)
            @namespace = @namespace.AddUsings(SyntaxFactory.UsingDirective(SyntaxFactory.ParseName(t.Namespace)));

            for (int i = 0; i < 1024; i++)
            {
                var classDeclaration = SyntaxFactory
                                    .ClassDeclaration($"{t.Name}_{Guid.NewGuid().ToString().Replace('-', '_')}")
                                    .AddModifiers(SyntaxFactory.Token(SyntaxKind.PublicKeyword))
                                    .AddBaseListTypes(SyntaxFactory.SimpleBaseType(SyntaxFactory.ParseTypeName(t.Name)));


                @namespace = @namespace.AddMembers(classDeclaration);
            }


            // Normalize and get code as string.
            var code = @namespace
                .NormalizeWhitespace()
                .ToFullString();
            return code;
        }
        public static Assembly Build(string code)
        {

            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(code);

            string assemblyName = Assembly.GetExecutingAssembly().FullName + Guid.NewGuid().ToString();// Path.GetRandomFileName();
            var references = new List<MetadataReference>();


            foreach (var item in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!item.IsDynamic && !string.IsNullOrWhiteSpace(item.Location))
                    references.Add(MetadataReference.CreateFromFile(item.Location));
            }
            CSharpCompilation compilation = CSharpCompilation.Create(
                assemblyName,
                syntaxTrees: new[] { syntaxTree },
                references: references,
                options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                );



            using (var ms = new MemoryStream())
            {
                EmitResult result = compilation.Emit(ms);

                if (!result.Success)
                {
                    IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic =>
                        diagnostic.IsWarningAsError ||
                        diagnostic.Severity == DiagnosticSeverity.Error);

                    foreach (Diagnostic diagnostic in failures)
                    {
                        Console.Error.WriteLine("\t{0}: {1}", diagnostic.Id, diagnostic.GetMessage());
                    }
                }
                else
                {
                    ms.Seek(0, SeekOrigin.Begin);
                    Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);
                    return assembly;
                }
            }
            return null;
        }
    }
}
