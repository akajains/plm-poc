using PLM.CmsService.Data;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Authentication.JwtBearer;
using System.IdentityModel.Tokens;
using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc.Filters;
using System.IdentityModel.Tokens.Jwt;

namespace CmsService.API
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();

            
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSingleton<IConfiguration>(sp => { return Configuration; });
            services.AddSingleton<IDbConnection, MongoConnection>(); //ToDo: Consider to implement pooling singleton pattern
            services.AddScoped<IRepository<CMSContent>, CMSRepository>();

            var scopePolicy = new AuthorizationPolicyBuilder()
                .RequireClaim("scope", "read")
                .Build();

            services.AddMvcCore(setup =>
            {
                setup.Filters.Add(new AuthorizeFilter(scopePolicy));
            })
            .AddJsonFormatters()
            .AddAuthorization();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseIISPlatformHandler();
            app.UseMvcWithDefaultRoute();
            app.UseMvc();                    
         
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            var certificate = new X509Certificate2(Convert.FromBase64String("MIIFFTCCAv2gAwIBAgIQJhSZrRiH2YZDr7NL/ND53zANBgkqhkiG9w0BAQ0FADAUMRIwEAYDVQQDEwlBamFpblJvb3QwHhcNMTUxMjMxMTQwMDAwWhcNMTkxMjMxMTQwMDAwWjAZMRcwFQYDVQQDHg4AKgAuAGwAbwBjAGEAbDCCAiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAJ/tB9Tu3yo1+es5YSnl6JYDdau3P8nXpihouC9S+hYaS1ckcIb2z0jBhsZNgQfipSFS9wxZXQTqf+7kTEFvYfav18WlgUM95ClEP5WUSsNd9/DXHswWnMtKk3H5Vw9gZIjf5W2QeBIhtUwUBLqkjXGG3jE0SV4lMKHBeBURRu6anhpb9urwTwUTK7x5t8mEvjii+DaMwFMYDeegoxz8LvvyE0FtGELmscyL2NQFF0WgF8tISFF7rjLk9mCfQcYVPHtkCwQpe2E2c3vCYpqeNTffQKJGEDFsneHBmdiQMSzeuqundRrvW2Dd1qaghd4uEqa1dYAZ41n6LlAVBUx9AQ9XmF3I6fksLpZ2k0Txu+H7aDrDxmEm9qbh3BhlGVte683cJ4e8a5Z8EwKHb/ZHgaa6j9Jdz81X1CWH0AZihzi93GSUy2XiUFLMcwE1Xef1qyar+VpcVrXCmKRbI1mlbe1VNlhiWxqUSZ45tBgPtsT5BMRBBtb2Xhlz91ejItTlkT65pNpaY3FvqPUWXrIlCAPGZzudgVSE84koq0Wdbt4TA0x05B5O4rT0x2qL+wSms9oQAJ9n+zQJ4PnSVBdjGORJVHUXFu1kfn9uGeW2axqeOuS3Ms+rdY8BuThRgBCLcttemF4vKtpp47NdtOqw7Ks1d4pkDdqgfQMxwq5xGzUdAgMBAAGjXjBcMBMGA1UdJQQMMAoGCCsGAQUFBwMBMEUGA1UdAQQ+MDyAECAxkcGrraau2dBl/WFRIiShFjAUMRIwEAYDVQQDEwlBamFpblJvb3SCEB3awmpWg1mGQrysRBuXtFcwDQYJKoZIhvcNAQENBQADggIBANLylW3fXLbd9iAqqhczhxBY0MtbcNQzmejoj5UNdIJBsDPFsbtYgq/q/WUawl4nTAF2v7WCrPjID1ssY0G1yb8i79FVrEzwJNXxXKm0cKujUcvJYUm2wOV/39WmJWd6uL+nJ2bioURjMgbdZKnSCR2Mos+1e0lAbBbnMosM55qVpZqtRewslo4+1AZCRAceXjsnGCTCR71rKdptvyDcFJq8POsKXFmajNl9VsU5SeRMxDQaB7W62IFpxkXZE7GJm0BRisdIsCyjKTRHlEEsj4ZjHoAWOGYBKnPb/gXVQ4VH7HBHOOC5uWuJI1Ra3+YF3eVSg8Bl+/qsqQK28izxl23ntqxjqr/RRbdnCrurHoy+1xC7iqplUQJuSk+k7dmvzJlB+bPVmSaKXGkm2zb8jSshrSXprl2l/bOlsU+PYoBNEe5UjIKC3nGR+88B8HmEQ0POKVjCl/qysRnGupvWFzD4qjgV0+0oyrP+ebeQyCQr47QQDylC9XdVqtyE95b/pUPzwPP61kiAU3YqOg4+pyWApzSBCvBml4nXfH8rQeALAPYWYepUMl99mwIANabaBgOSFKBhEXO1j+s3jsHxpOLHkWtxIVhNNTXQaQWdIPSuH1/QrwcK45rt5cdgg7G9kh7SywLAEXuXXKER7sBrBtB6jhvLARUaimzm4Mx9awEX"));


            ////oAuthorization
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {

                Authority = "http://localhost:64705",
                Audience = "http://localhost:64705/resources",
                RequireHttpsMetadata =false,
                AutomaticAuthenticate = true,
                TokenValidationParameters = new TokenValidationParameters
                {
                    
                    //ValidIssuer = "http://localhost:64705/",
                    //ValidAudience = "http://localhost:64705/resources",
                    //IssuerSigningKey = new X509SecurityKey(certificate),
                    NameClaimType = "name"

                }

                
            });

               





        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
