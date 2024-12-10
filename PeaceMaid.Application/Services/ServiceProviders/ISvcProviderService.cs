using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceMaid.Application.Services.ServiceProviders
{
    public interface ISvcProviderService
    {
        Task<ServiceResponse> AddAsync(ServiceProviderDTO serviceProviderDTO, MultipartFormDataContent content);
        Task<ServiceResponse> DeleteAsync(int Id);
        Task<List<ServiceProvider>> GetAsync();
        Task<ServiceResponse> UpdateAsync(ServiceProviderDTO serviceProviderDTO);
        Task<ServiceProvider?> GetProviderAsync(int userId);
    }
}
