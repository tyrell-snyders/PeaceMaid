using Braintree;
using Microsoft.EntityFrameworkCore;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;
using BraintreeService = PeaceMaid.Infrastructure.Middleware.Payments.BraintreeService;
using PaymentMethod = PeaceMaid.Domain.Entities.PaymentMethod;

namespace PeaceMaid.Infrastructure.Implementation
{
    public static class PaymentMessages
    {
        public const string PaymentNotFound = "Payment ID does not exist.";
        public const string PaymentNotPending = "Payment cannot be processed. Current status is not Pending.";
        public const string PaymentProcessed = "Payment processed successfully.";
    }

    public class PaymentRepo(AppDbContext context, BraintreeService braintreeService) : IPayment
    {
        private readonly AppDbContext _context = context;
        private readonly BraintreeService _braintreeService = braintreeService;


        public async Task<ServiceResponse> CancelAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
                return new(false, PaymentMessages.PaymentNotFound);

            _context.Payments.Remove(payment);
            await SaveChangesAsync();

            return new(true, "Payment cancelled successfully.");

        }

        public async Task<HashSet<string>> GetPaymentMethodsAsync()
        {
            var paymentMethods = Enum.GetNames(typeof(PaymentMethod))
                              .Select(method => method.ToUpper())
                              .ToHashSet();
            return await Task.FromResult(paymentMethods);
        }

        public string? GenerateClientToken() => _braintreeService.GenereteClientToken();

        public async Task<DataResponse> PayAsync(int id, string nonce)
        {
            //Todo: Use Braintree API to handle payment methods
            var payment = await _context.Payments
                                        .Include(p => p.User)
                                        .Include(p => p.Booking)
                                        .FirstOrDefaultAsync(p => p.PaymentId == id);
            if (payment == null)
                return new(false, PaymentMessages.PaymentNotFound, null);
            if (payment.Status != PaymentStatus.Pending)
                return new(false, PaymentMessages.PaymentNotPending, null);

            var booking = await _context.Bookings.FindAsync(payment.BookingId);
            if (booking == null)
                return new(false, "Booing not found.", null);

            var amount = booking.TotalCost;

            // Braintree Processing
            var paymentResponse = await _braintreeService.ProcessPaymentAsync(amount, nonce);

            // Update payment in db
            payment.Method = PaymentMethod.Credit_Card;
            payment.Status = PaymentStatus.Completed;
            payment.PaymentDate = DateTime.UtcNow;

            await SaveChangesAsync();

            return new(true, paymentResponse.Message, paymentResponse.Data);
        }

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
