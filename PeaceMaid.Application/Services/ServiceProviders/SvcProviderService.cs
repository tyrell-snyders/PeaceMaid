using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

using System.Net.Http.Json;

namespace PeaceMaid.Application.Services.ServiceProviders
{
    public class SvcProviderService(HttpClient httpClient) : ISvcProviderService
    {
        private readonly HttpClient _client = httpClient;

        public async Task<ServiceResponse> AddAsync(ServiceProviderDTO serviceProviderDTO, MultipartFormDataContent content)
        {
            var response = await _client.PostAsync("api/ServiceProvider", content);

            if (response.IsSuccessStatusCode)
            {
                return new (true, "Service provider added successfully." );
            }

            var error = await response.Content.ReadAsStringAsync();
            return new (false, error );
        }

        public Task<ServiceResponse> DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ServiceProvider>> GetAsync() =>
            await _client.GetFromJsonAsync<List<ServiceProvider>>("api/ServiceProvider");

        public Task<ServiceProvider?> GetProviderAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateAsync(ServiceProviderDTO serviceProviderDTO)
        {
            throw new NotImplementedException();
        }
    }
}
