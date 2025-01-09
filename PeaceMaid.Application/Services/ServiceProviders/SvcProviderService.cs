using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;
using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace PeaceMaid.Application.Services.ServiceProviders
{
    public class SvcProviderService(HttpClient httpClient, IJSRuntime jSRuntime) : ISvcProviderService
    {
        private readonly HttpClient _client = httpClient;
        private readonly IJSRuntime _jsRuntime = jSRuntime;

        public async Task<ServiceResponse> AddAsync(StringContent? content)
        {
            var response = await _client.PostAsync("api/ServiceProvider", content);

            if (response.IsSuccessStatusCode)
            {
                return new(true, "Service provider added successfully.");
            }

            var error = response.ReasonPhrase;
            return new(false, error);
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

        public async Task<List<Service>> GetServicesByProvider(int providerId) =>
            await _client.GetFromJsonAsync<List<Service>>($"api/ServiceProvider/services{providerId}");

        public Task<ServiceResponse> UpdateAsync(ServiceProviderDTO serviceProviderDTO)
        {
            throw new NotImplementedException();
        }
    }
}
