using Microsoft.Extensions.DependencyInjection;
using PeaceMaid.Application;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Application.Interfaces.Authentication;
using PeaceMaid.Infrastructure.Implementation;
using PeaceMaid.Infrastructure.Implementation.Authentication;

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

            return services;
        }
    }
}
