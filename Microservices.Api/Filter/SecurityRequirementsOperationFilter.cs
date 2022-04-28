using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;

namespace Microservices.Api.Filter
{
    public class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Responses.Add("401", new OpenApiResponse { Description = "Http Code - 401: Unauthorized" });
            operation.Responses.Add("403", new OpenApiResponse { Description = "Http Code - 403: Forbidden" });

            operation.Security = new List<OpenApiSecurityRequirement>{
                    new OpenApiSecurityRequirement
                    {
                        [ new OpenApiSecurityScheme {
                            Reference = new OpenApiReference{
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        }] = new [] { "DevApi", "UatApi" }
                    }
                };
        }
    }
}
