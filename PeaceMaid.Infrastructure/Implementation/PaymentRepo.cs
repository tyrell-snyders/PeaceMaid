using Microsoft.EntityFrameworkCore;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;

namespace PeaceMaid.Infrastructure.Implementation
{
    public static class PaymentMessages
    {
        public const string PaymentNotFound = "Payment ID does not exist.";
        public const string PaymentNotPending = "Payment cannot be processed. Current status is not Pending.";
        public const string PaymentProcessed = "Payment processed successfully.";
    }

    public class PaymentRepo(AppDbContext context) : IPayment
    {
        private readonly AppDbContext _context = context;

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

        public async Task<ServiceResponse> PayAsync(int id, PaymentMethod paymentMethod)
        {
            //Todo: Use Braintree API to handle payment methods
            var payment = await _context.Payments
                                        .Include(p => p.User)
                                        .Include(p => p.Booking)
                                        .FirstOrDefaultAsync(p => p.PaymentId == id);

            if (payment == null)
                return new(false, PaymentMessages.PaymentNotFound);

            if (payment.Status != PaymentStatus.Pending)
                return new(false, PaymentMessages.PaymentNotPending);

            payment.Method = paymentMethod;
            payment.Status = PaymentStatus.Completed;
            payment.PaymentDate = DateTime.UtcNow;

            await SaveChangesAsync();

            return new(true, PaymentMessages.PaymentProcessed);
        }

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
