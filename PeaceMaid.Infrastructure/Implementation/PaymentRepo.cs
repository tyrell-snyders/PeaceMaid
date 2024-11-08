using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;

namespace PeaceMaid.Infrastructure.Implementation
{
    public class PaymentRepo(AppDbContext context) : IPayment
    {
        private readonly AppDbContext _context = context;

        public async Task<ServiceResponse> CancelAsync(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
                return new(false, "Payment Id does not exist.");

            _context.Payments.Remove(payment);
            await SaveChangesAsync();

            return new(true, "Removed");
        }

        public async Task<ServiceResponse> PayAsync(int id, PaymentMethod paymentMethod)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return new(false, "Payment Id does not exist.");

            payment.Method = paymentMethod;
            payment.Status = PaymentStatus.Completed;
            await SaveChangesAsync();

            return new(true, "Paid");
        }

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
