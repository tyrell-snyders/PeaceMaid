using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PeaceMaid.Application.Services.Svcs
{
    public class SvcsServices(HttpClient httpClient) : ISvcsService
    {
        private readonly HttpClient _client = httpClient;

        public Task<ServiceResponse> AddAsync(Service service)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Service?>> GetAsync() =>
            await _client.GetFromJsonAsync<List<Service?>>("api/Service");

        public Task<Service?> GetServiceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateAsync(Service service)
        {
            throw new NotImplementedException();
        }
    }
}
