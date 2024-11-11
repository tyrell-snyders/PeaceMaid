using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application.Interfaces
{
    public interface IPayment
    {
        Task<ServiceResponse> PayAsync(int id, PaymentMethod paymentMethod);
        Task<ServiceResponse> CancelAsync(int id);
        Task<HashSet<string>> GetPaymentMethodsAsync();
    }
}
