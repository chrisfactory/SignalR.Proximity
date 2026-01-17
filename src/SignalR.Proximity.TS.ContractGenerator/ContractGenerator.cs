using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SignalR.Proximity.TS.ContractGenerator
{
    [Generator]
    public class TypeScriptContractGenerator : IIncrementalGenerator
    {
        private const string AttributeName = "SignalR.Proximity.ProximityTypeScriptCodeSyncAttribute";

        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // 0. Inject Attribute
            context.RegisterPostInitializationOutput(ctx => ctx.AddSource("ProximityTypeScriptCodeSyncAttribute.g.cs", @"
using System;

namespace SignalR.Proximity
{
    [AttributeUsage(AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class ProximityTypeScriptCodeSyncAttribute : Attribute
    {
        public string TargetPath { get; }

        public ProximityTypeScriptCodeSyncAttribute(string targetPath)
        {
            TargetPath = targetPath;
        }
    }
}"));

            // 1. Find interfaces with the attribute
            var pipelines = context.SyntaxProvider.ForAttributeWithMetadataName(
                AttributeName,
                predicate: (node, _) => node is InterfaceDeclarationSyntax,
                transform: (ctx, _) => GetContractInfo(ctx))
                .Where(x => x != null);

            // 2. Generate and Write
            context.RegisterSourceOutput(pipelines, (spc, source) =>
            {
                if (source == null) return;
                try
                {
                    GenerateAndWrite(spc, source, context);
                }
                catch (Exception ex)
                {
                    // Report diagnostic for error
                    var descriptor = new DiagnosticDescriptor(
                       "PRXGEN001",
                       "Error Generating TypeScript",
                       $"Error generating TS for {source.InterfaceName}: {ex.Message} {ex.StackTrace}",
                       "ProximityGenerator",
                       DiagnosticSeverity.Warning,
                       isEnabledByDefault: true);
                    spc.ReportDiagnostic(Diagnostic.Create(descriptor, Location.None));
                }
            });
        }

        private class ContractInfo
        {
            public ContractInfo(INamedTypeSymbol interfaceSymbol, string targetPath, string interfaceName, string ns, string? projectDirectory)
            {
                InterfaceSymbol = interfaceSymbol;
                TargetPath = targetPath;
                InterfaceName = interfaceName;
                Namespace = ns;
                ProjectDirectory = projectDirectory;
            }
            public INamedTypeSymbol InterfaceSymbol;
            public string TargetPath;
            public string InterfaceName;
            public string Namespace;
            public string? ProjectDirectory;
        }

        private ContractInfo? GetContractInfo(GeneratorAttributeSyntaxContext context)
        {
            var folder = GetProjectDirectory(context.SemanticModel.SyntaxTree);
            var att = context.Attributes.FirstOrDefault(a => a.AttributeClass?.ToDisplayString() == AttributeName);
            if (att == null) return null;

            var targetPath = att.ConstructorArguments[0].Value?.ToString()!;

            return new ContractInfo(
                (INamedTypeSymbol)context.TargetSymbol,
                targetPath, context.TargetSymbol.Name,
                context.TargetSymbol.ContainingNamespace.ToDisplayString(),
                folder);
            ;
        }

        private string? GetProjectDirectory(SyntaxTree tree)
        {
            // Simple heuristic: get directory of the source file
            var path = tree.FilePath;
            if (string.IsNullOrEmpty(path)) return null;
            return Path.GetDirectoryName(path);
        }

        private void GenerateAndWrite(SourceProductionContext spc, ContractInfo info, IncrementalGeneratorInitializationContext globalCtx)
        {
            if (string.IsNullOrEmpty(info.ProjectDirectory)) return;

            var fullPath = Path.Combine(info.ProjectDirectory, info.TargetPath)!;
            var content = GenerateTypeScript(info.InterfaceSymbol);

            // Writing to disk (Side Effect!)
            // We check if content changed to avoid spamming IO
            if (File.Exists(fullPath))
            {
                var existing = File.ReadAllText(fullPath);
                if (existing == content) return;
            }

            // Determine directory
            var dir = Path.GetDirectoryName(fullPath)!;
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            File.WriteAllText(fullPath, content);
        }

        private string GenerateTypeScript(INamedTypeSymbol symbol)
        {
            var sb = new StringBuilder();
            sb.AppendLine("// Auto-generated by SignalR.Proximity.TS.ContractGenerator");
            sb.AppendLine("// Do not edit manually.");
            sb.AppendLine("");

            // 1. Collect Dependent Types (DTOs)
            var typeCollectorSet = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
            var typeCollectorList = new List<INamedTypeSymbol>();

            CollectTypes(symbol, typeCollectorList, typeCollectorSet);

            // Remove the contract itself if added (it shouldn't be generated as DTO)
            if (typeCollectorSet.Contains(symbol))
            {
                typeCollectorList.Remove(symbol);
                typeCollectorSet.Remove(symbol);
            }

            // 2. Generate DTOs & Enums
            foreach (var type in typeCollectorList)
            {
                GenerateTypeDefinition(sb, type, isContract: false);
            }

            // 3. Generate Contract Interface
            GenerateTypeDefinition(sb, symbol, isContract: true);

            // 4. Generate Signatures
            GenerateSignatures(sb, symbol);

            // 5. Generate Path Constant (using fullname lowercased as primitive convention from sample)
            // Sample convention: "sample.signalr.proximity.toaster.ischoolcontract"
            var pathConst = symbol.ToDisplayString().ToLowerInvariant();

            // Extract a variable name part, e1.g. ISchoolContract -> schoolPath
            var varPrefix = symbol.Name.StartsWith("I") ? symbol.Name.Substring(1) : symbol.Name;
            varPrefix = char.ToLower(varPrefix[0]) + varPrefix.Substring(1);

            sb.AppendLine($"export const {varPrefix}Path = \"{pathConst}\";");

            return sb.ToString();
        }

        private void CollectTypes(ITypeSymbol typeSymbol, List<INamedTypeSymbol> orderedList, HashSet<INamedTypeSymbol> uniqueSet)
        {
            if (typeSymbol is IArrayTypeSymbol arrayType)
            {
                CollectTypes(arrayType.ElementType, orderedList, uniqueSet);
                return;
            }

            if (typeSymbol is INamedTypeSymbol named)
            {
                if (named.SpecialType != SpecialType.None) return; // Ignore System types like int, string

                // Handle Generics (List<T>, IEnumerable<T>)
                if (named.IsGenericType)
                {
                    foreach (var arg in named.TypeArguments)
                    {
                        CollectTypes(arg, orderedList, uniqueSet);
                    }
                }

                // If it's a class or struct (NOT ENUM anymore), and not already collected
                if (named.TypeKind == TypeKind.Class || named.TypeKind == TypeKind.Struct || named.TypeKind == TypeKind.Interface)
                {
                    // Filter out System namespaces loosely if needed, or rely on SpecialType check above
                    if (named.ContainingNamespace.ToDisplayString().StartsWith("System")) return;

                    if (!uniqueSet.Contains(named))
                    {
                        uniqueSet.Add(named);

                        // Recurse into members
                        foreach (var member in named.GetMembers())
                        {
                            if (member is IMethodSymbol method)
                            {
                                foreach (var arg in method.Parameters)
                                {
                                    CollectTypes(arg.Type, orderedList, uniqueSet);
                                }
                                if (!method.ReturnsVoid)
                                {
                                    CollectTypes(method.ReturnType, orderedList, uniqueSet);
                                }
                            }
                            else if (member is IPropertySymbol prop)
                            {
                                CollectTypes(prop.Type, orderedList, uniqueSet);
                            }
                        }

                        orderedList.Add(named);
                    }
                }
            }
        }

        private void GenerateTypeDefinition(StringBuilder sb, INamedTypeSymbol symbol, bool isContract)
        {
            // Determine if it should be a class or interface
            // Contracts are always interfaces
            // DTOs: if Class/Struct -> Class, if Interface -> Interface

            bool isClass = !isContract && (symbol.TypeKind == TypeKind.Class || symbol.TypeKind == TypeKind.Struct);
            string keyword = isClass ? "class" : "interface";

            sb.Append($"export {keyword} {symbol.Name} ");
            sb.AppendLine("{");

            if (isContract)
            {
                foreach (var member in symbol.GetMembers().OfType<IMethodSymbol>())
                {
                    sb.Append($"    {member.Name}(");
                    sb.Append(string.Join(", ", member.Parameters.Select(p => $"{p.Name}: {ToTsType(p.Type)}")));
                    sb.AppendLine("): void;");
                }
            }
            else
            {
                // DTO Mode (Properties)
                foreach (var member in symbol.GetMembers().OfType<IPropertySymbol>())
                {
                    string access = isClass ? "public " : "";
                    string assertion = isClass ? "!" : "";
                    sb.AppendLine($"    {access}{member.Name}{assertion}: {ToTsType(member.Type)};");
                }
            }

            sb.AppendLine("}");
            sb.AppendLine("");
        }

        private void GenerateSignatures(StringBuilder sb, INamedTypeSymbol symbol)
        {
            var varPrefix = symbol.Name.StartsWith("I") ? symbol.Name.Substring(1) : symbol.Name;
            varPrefix = char.ToLower(varPrefix[0]) + varPrefix.Substring(1);

            sb.AppendLine($"export const {varPrefix}Signatures = {{");
            var methods = symbol.GetMembers().OfType<IMethodSymbol>().ToList();
            for (int i = 0; i < methods.Count; i++)
            {
                var m = methods[i];
                // Gen Signature: "Void ShowInformation(Sample.SignalR.Proximity.Toaster.ToasterRequest)"
                // ReturnType Name(ArgTypes)

                var returnType = m.ReturnsVoid ? "Void" : GetSystemTypeName(m.ReturnType);
                var args = string.Join(", ", m.Parameters.Select(p => GetSystemTypeName(p.Type)));
                var sig = $"{returnType} {m.Name}({args})";

                sb.Append($"    {m.Name}: \"{sig}\"");
                if (i < methods.Count - 1) sb.Append(",");
                sb.AppendLine("");
            }
            sb.AppendLine("};");
            sb.AppendLine("");
        }

        private string GetSystemTypeName(ITypeSymbol type)
        {
            switch (type.SpecialType)
            {
                case SpecialType.System_String: return "System.String";
                case SpecialType.System_Int32: return "System.Int32";
                case SpecialType.System_Double: return "System.Double";
                case SpecialType.System_Boolean: return "System.Boolean";
                case SpecialType.System_Void: return "Void";
            }
            return type.ToDisplayString(); // Fallback for complex types
        }

        private string ToTsType(ITypeSymbol type)
        {
            if (type.TypeKind == TypeKind.Enum) return "string";

            switch (type.SpecialType)
            {
                case SpecialType.System_String: return "string";
                case SpecialType.System_Int32:
                case SpecialType.System_Double:
                case SpecialType.System_Single:
                case SpecialType.System_Decimal: return "number";
                case SpecialType.System_Boolean: return "boolean";
                case SpecialType.System_DateTime: return "string"; // simplistic
                case SpecialType.System_Void: return "void";
            }

            // Primitive-ish types that map to string
            if (type.ContainingNamespace?.Name == "System" && (type.Name == "Guid" || type.Name == "DateTimeOffset" || type.Name == "TimeSpan"))
            {
                return "string";
            }

            if (type.TypeKind == TypeKind.Array) return ToTsType(((IArrayTypeSymbol)type).ElementType) + "[]";

            // Handle IEnumerable<T>, List<T> -> T[]
            // Handle Dictionary<K, V> -> Record<K, V>
            if (type is INamedTypeSymbol named && named.IsGenericType)
            {
                var name = named.Name;

                // Nullable Support
                if (name == "Nullable")
                {
                    var arg = named.TypeArguments.FirstOrDefault();
                    if (arg != null)
                    {
                        return ToTsType(arg) + " | null";
                    }
                }

                // Dictionary Support
                if (name == "Dictionary" || name == "IDictionary" || name == "IReadOnlyDictionary")
                {
                    if (named.TypeArguments.Length == 2)
                    {
                        var keyType = named.TypeArguments[0];
                        var valueType = named.TypeArguments[1];
                        return $"Record<{ToTsType(keyType)}, {ToTsType(valueType)}>";
                    }
                }

                // Array/List Support
                if (name == "List" || name == "IEnumerable" || name == "ICollection" || name == "IList" || name == "IReadOnlyList" || name == "IReadOnlyCollection")
                {
                    var arg = named.TypeArguments.FirstOrDefault();
                    if (arg != null)
                    {
                        return ToTsType(arg) + "[]";
                    }
                }
            }

            return type.Name; // Hope matches the DTO interface name
        }
    }
}
