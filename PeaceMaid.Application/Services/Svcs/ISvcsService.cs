using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeaceMaid.Application.Services.Svcs
{
    public interface ISvcsService
    {
        Task<List<Service>> GetAsync();
        Task<ServiceResponse> AddAsync(Service service);
        Task<ServiceResponse> UpdateAsync(Service service);
        Task<ServiceResponse> DeleteAsync(int id);
        Task<Service?> GetServiceAsync(int id);
    }
}
