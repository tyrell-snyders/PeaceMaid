using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application
{
    public interface IService
    {
        Task<List<Service>> GetAsync();
        Task<ServiceResponse> AddAsync(Service service);
        Task<ServiceResponse> UpdateAsync(Service service);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<Service?> GetServiceAsync(int id);
    }
}
