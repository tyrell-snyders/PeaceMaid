using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application.Interfaces
{
    public interface ISProvider
    {
        Task<ServiceResponse> AddAsync(ServiceProviderDTO serviceProviderDTO);
        Task<ServiceResponse> DeleteAsync(int Id);
        Task<List<ServiceProvider>> GetAsync();
        Task<ServiceResponse> UpdateAsync(ServiceProvider serviceProviderDTO);
        Task<ServiceProvider?> GetProviderAsync(int userId);
        Task<List<Service>> GetServicesAsync(int spId);
    }
}
