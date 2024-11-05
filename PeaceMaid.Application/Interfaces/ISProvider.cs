using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application.Interfaces
{
    public interface ISProvider
    {
        Task<ServiceResponse> AddAsync(ServiceProvider serviceProvider);
        Task<ServiceResponse> DeleteAsync(int Id);
        Task<List<ServiceProvider>> GetAsync();
        Task<ServiceResponse> UpdateAsync(ServiceProvider serviceProvider);
        Task<ServiceProvider?> GetProviderAsync(int userId);
    }
}
