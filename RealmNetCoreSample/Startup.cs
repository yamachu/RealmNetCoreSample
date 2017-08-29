using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealmNetCoreSample.Services;
using RealmNetCoreSample.Middlewares;

namespace RealmNetCoreSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IRealmProviderService>(new RealmProviderService(/* ここで設定ファイルを渡したり */));
            services.AddSingleton<IPasswordHashService, PasswordHashService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseWhen(
                context => context.Request.Path.StartsWithSegments("/api/database", StringComparison.OrdinalIgnoreCase),
                builder =>
                {
                    builder.UseMiddleware<CustomTokenAuthenticationMiddleware>();
                }
            );

            app.UseWhen(
                context => context.Request.Path.StartsWithSegments("/admin", StringComparison.OrdinalIgnoreCase),
                builder =>
                {
                    builder.UseMiddleware<BasicAuthenticationMiddleware>();
                }
            );

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });
        }
    }
}
