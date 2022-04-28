using IdentityServer.Core.Client;
using IdentityServer.Core.Resource;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace IdentityServer
{
    public class Startup
    {
        private string IdentityServerAllowSpecificOrigins = "_identityServerAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            SharedKernel.HttpContext.HttpContextExtension.ServiceCollection = services;

            services.AddCors(options => {
                options.AddPolicy(IdentityServerAllowSpecificOrigins, policy => {
                    policy.WithOrigins(
                        $"https://localhost:44328",
                        $"http://localhost:55836/"
                        )
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowAnyOrigin();
                });
            });

            services.AddSingleton<ICorsPolicyService>((container) => {
                var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
                return new DefaultCorsPolicyService(logger)
                {
                    AllowedOrigins = { 
                        $"https://localhost:44328",
                        $"http://localhost:55836/"
                    },
                    AllowAll = true
                };
            });

            services.AddIdentityServer()
                .AddInMemoryIdentityResources(Resources.IdentityResources)
                .AddInMemoryClients(Clients.GetClients())
                .AddInMemoryApiResources(Resources.GetApiResources())
                .AddInMemoryApiScopes(Resources.GetApiScopes())
                // Create temp encripty key for developer stage.(tempkey.jwk)
                .AddDeveloperSigningCredential()
                // Add the test user info.
                .AddTestUsers(Clients.TestUsers);

            //services.AddControllers();
            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "IdentityServer", Version = "v1" });
                
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityServer v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(IdentityServerAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
