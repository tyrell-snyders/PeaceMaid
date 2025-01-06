using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application.Interfaces
{
    public interface IPayment
    {
        Task<DataResponse> PayAsync(int id, string nonce);
        Task<ServiceResponse> CancelAsync(int id);
        Task<HashSet<string>> GetPaymentMethodsAsync();
        string? GenerateClientToken();
    }
}
