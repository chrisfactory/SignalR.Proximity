using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.SignalR;
using Sample.SignalR.Proximity.Toaster;
using SignalR.Proximity.Hosting;

namespace SignalR.Proximity.Hubs
{
    /// <summary>
    /// Définition standard du startup web pour Startup
    /// </summary>
    public class Startup
    {
        public Startup(IWebHostEnvironment env, IConfiguration configuration)
        {
            Environment = env;
            Configuration = configuration;
        }
        public IWebHostEnvironment Environment { get; private set; }
        public IConfiguration Configuration { get; private set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers()
                .AddNewtonsoftJson();

            services.AddSignalR();
             
            services.AddControllersWithViews(); 
        }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // app.UseHttpsRedirection(); 
            app.UseStaticFiles();
            app.UseRouting();
            //Si tu inverse ces deux prochaines lignes, ça ne marche plus en .netcore3.0...
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ProximityHub<IToastNotificationsContract>>("/hubs/sample.signalr.proximity.toaster.itoastnotificationscontract");
             //   endpoints.MapHub<ProximityHub<IToastNotificationsContract>>("/hubs/sample.signalr.proximity.toaster.itoastnotificationscontract");
            }); 
        }

    }
}





