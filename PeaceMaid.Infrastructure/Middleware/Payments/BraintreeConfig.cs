using Braintree;
using Microsoft.Extensions.Configuration;

namespace PeaceMaid.Infrastructure.Middleware.Payments
{
    public class BraintreeConfig
    {
        public IBraintreeGateway Gateway { get; private set; }

        public BraintreeConfig(IConfiguration configuration)
        {
            Gateway = new BraintreeGateway(
                configuration["Braintree:Environment"],
                configuration["Braintree:MerchantId"],
                configuration["Braintree:PublicKey"],
                configuration["Braintree:PrivateKey"]
            );
        }
    }
}
