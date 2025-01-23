using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application.Services.ServiceProviders
{
    public interface ISvcProviderService
    {
        Task<ServiceResponse> AddAsync(HttpContent content);
        Task<ServiceResponse> DeleteAsync(int Id);
        Task<List<ServiceProvider>> GetAsync();
        Task<ServiceResponse> UpdateAsync(ServiceProviderDTO serviceProviderDTO);
        Task<ServiceProvider?> GetProviderAsync(int userId);
        Task<List<Service>> GetServicesByProvider(int providerId);
        Task<int> GetUserId();
    }
}
