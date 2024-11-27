using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;

namespace PeaceMaid.Presentation.WebAPI.Middleware
{
    public class AuthorizeCheckOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var hasAuthorize = context.MethodInfo
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Any()
                || context.MethodInfo.DeclaringType
                .GetCustomAttributes(true)
                .OfType<AuthorizeAttribute>()
                .Any();

            if (hasAuthorize)
            {
                operation.Security = new List<OpenApiSecurityRequirement>
                {
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                        }
                    }
                };
            }
        }
    }
}
