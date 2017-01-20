using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lawyers.Contracts;
using Lawyers.Contracts.Models;
using Lawyers.Service;
using Lawyers.WebApp.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Lawyers.WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {   
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            AutoMapper.Mapper.Initialize(expression => {
                expression.CreateMap<Lawyer, LawyerModel>();
            });

            services.AddTransient<ILawyersPageFactory,DefaultLageFactory>();
            services.AddTransient<ILawyersService, LawyerServiceMock>();
            services.AddTransient<ILookupsService,LookupsesServiceMock>();
            // Add framework services.
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "list",
                    template: "{param?}",
                    defaults: new
                    {
                        Controller = "Lawyers",
                        Action = "Index"
                    });
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"

                    );
            });
        }
    }
}
