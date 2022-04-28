using IdentityServer4.AccessTokenValidation;
using Microservices.Api.Filter;
using Microservices.Api.Logging.Interface;
using Microservices.Api.Logging.Repository;
using Microservices.Infra.DBContext.Database;
using Microservices.Infra.DBContext.Interface;
using Microservices.Infra.DBContext.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Microservices.Api
{
    public class Startup
    {
        private string ApiAllowSpecificOrigins = "_apiAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Database connection setting.
            services.AddDbContext<DemoDbContext>(option =>
            {
                //option.UseInMemoryDatabase("Test");
                option.UseSqlServer(Configuration.GetConnectionString("DB_EntityString"));
            }, ServiceLifetime.Transient);

            //AOP Scoped
            services.AddScoped(typeof(ILogRepository), typeof(LogRepository));
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            services.AddScoped(typeof(IGenericTypeRepository<>), typeof(GenericTypeRepository<>));           

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = $"https://{Configuration["Auth0:Domain"]}";
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateAudience = false
                    };
                });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Microservices.Api",
                    Description = "Microservices API Demo",
                    Version = "v1"
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                services.AddCors(options => {
                    options.AddPolicy(ApiAllowSpecificOrigins, policy => {
                        policy.WithOrigins($"https://{Configuration["Auth0:Domain"]}")
                        .AllowAnyHeader()
                        .AllowAnyOrigin();
                    });
                });

                var securitySchema = new OpenApiSecurityScheme { 
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows { 
                        AuthorizationCode = new OpenApiOAuthFlow { 
                            AuthorizationUrl = new Uri($"https://{Configuration["Auth0:Domain"]}/connect/authorize", UriKind.RelativeOrAbsolute),
                            TokenUrl = new Uri($"https://{Configuration["Auth0:Domain"]}/connect/token", UriKind.RelativeOrAbsolute),
                            Scopes = new Dictionary<string, string> {
                                { "DevApi", "Authorization - CRUD" },
                                { "UatApi", "UAT Authorization - CRUD"}
                            }
                        }
                    }
                };

                c.AddSecurityDefinition("oauth2", securitySchema);
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory )
        {
            if (!env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.UseStaticFiles();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Microservices.Api v1");

                // Additional OAuth settings (See https://github.com/swagger-api/swagger-ui/blob/v3.10.0/docs/usage/oauth2.md)
                c.OAuthClientId("Dev-User");
                c.OAuthClientSecret("dev_Secret");
                c.OAuthAppName("Microservices.Api");
                c.OAuthScopeSeparator(" ");

                c.OAuthUsePkce();
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Add log file path
            var path = Directory.GetCurrentDirectory();
            loggerFactory.AddFile($"{path}\\Logs\\Log.txt");

            // Test
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None,
                MinimumSameSitePolicy = SameSiteMode.None,
                Secure = CookieSecurePolicy.Always
            });

            app.UseCors(ApiAllowSpecificOrigins);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
