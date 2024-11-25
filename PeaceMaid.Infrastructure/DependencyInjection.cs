using Microsoft.Extensions.DependencyInjection;
using PeaceMaid.Application;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Application.Interfaces.Authentication;
using PeaceMaid.Infrastructure.Implementation;
using PeaceMaid.Infrastructure.Implementation.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using PeaceMaid.Infrastructure.Middleware;

namespace PeaceMaid.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISProvider, ServiceProviderRepo>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUser, UserRepo>();
            services.AddScoped<IService, ServiceRepo>();
            services.AddScoped<IBooking, BookingRepo>();
            services.AddScoped<IPayment, PaymentRepo>();
            services.AddScoped<IReview, ReviewRepo>();

            // JWT auth
            services.AddScoped<UserAuth>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Key")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            return services;
        }
    }
}
