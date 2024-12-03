using Braintree;
using PeaceMaid.Application.DTOs;

namespace PeaceMaid.Infrastructure.Middleware.Payments
{
    public class BraintreeService
    {
        private readonly IBraintreeGateway _gateway;

        public BraintreeService(BraintreeConfig conf)
        {
            _gateway = conf.Gateway;
        }

        public async Task<PaymentResponse> ProcessPaymentAsync(decimal amount, string nonce)
        {
            // create a transaction
            var request = new TransactionRequest
            {
                Amount = amount,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true, // Automatically capture the payment
                }
            };

            var result = await _gateway.Transaction.SaleAsync(request);

            if (result.IsSuccess())
            {
                return new(true, "Payment Successful", result.Target);
            }

            var errMessage = string.Join(", ", result.Errors.DeepAll().Select(e => e.Message));

            var clientToken = _gateway.ClientToken.Generate();
            return new(false, $"Payment failed: {errMessage}", null);
        }

        public string? GenereteClientToken() => _gateway.ClientToken.Generate();
    }
}
