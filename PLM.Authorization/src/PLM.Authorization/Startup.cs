using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using IdentityServer4.Core.Validation;
using IdentityServer4.Core.Configuration;
using PLM.Authorization.UI.Login;
using PLM.Authorization.UI;
using Microsoft.Extensions.Logging;

namespace PLM.Authorization
{
    public class Startup
    {       
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)        {

            var policy = new Microsoft.AspNet.Cors.Infrastructure.CorsPolicy();
            services.AddCors();            
            policy.Headers.Add("*");
            policy.Methods.Add("*");
            policy.Origins.Add("*");
            policy.SupportsCredentials = true;
            services.AddCors(x => x.AddPolicy("corsGlobalPolicy", policy));

            var inMemoryManager = new InMemoryManager();

            var builder = services.AddIdentityServer(options =>
            {
                options.SigningCertificate = Certificate.Get();
                options.Endpoints.EnableEndSessionEndpoint = true;
                options.AuthenticationOptions = new AuthenticationOptions
                {
                    EnableSignOutPrompt = false
                };

            });

            builder.AddInMemoryClients(inMemoryManager.GetClients());
            builder.AddInMemoryScopes(inMemoryManager.GetScopes());
            builder.AddInMemoryUsers(inMemoryManager.GetUsers());



            // for the UI
            services
                .AddMvc()
                .AddRazorOptions(razor =>
                {
                    razor.ViewLocationExpanders.Add(new CustomViewLocationExpander());
                });
            services.AddTransient<LoginService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseCors("corsGlobalPolicy");

            loggerFactory.AddConsole(LogLevel.Verbose);
            loggerFactory.AddDebug(LogLevel.Verbose);

            app.UseDeveloperExceptionPage();

            app.UseIISPlatformHandler(); // ToDo:  Do we really need this now or in future?
            app.UseIdentityServer();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("PM Authorization services are up and running.");
            //});
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
