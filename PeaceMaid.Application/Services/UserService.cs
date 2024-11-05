using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;
using System.Net.Http.Json;

namespace PeaceMaid.Application.Services
{
    public class UserService(HttpClient httpClient) : IUserService
    {
        private readonly HttpClient _httpClient = httpClient;
        public async Task<ServiceResponse> AddAsync(User user)
        {
            var data = await _httpClient.PostAsJsonAsync("api/employee", user);
            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();

            return response!;
        }

        public async Task<ServiceResponse> DeleteAsync(int Id)
        {
            var data = await _httpClient.DeleteAsync($"api/employee/{Id}");
            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();

            return response!;
        }

        public async Task<List<User>> GetAllAsync() => 
            await _httpClient.GetFromJsonAsync<List<User>>("api/employee")!;

        public async Task<User> GetByIdAsync(int Id) =>
            await _httpClient.GetFromJsonAsync<User>($"api/employee/{Id}")!;

        public async Task<ServiceResponse> UpdateAsync(User user)
        {
            var data = await _httpClient.PutAsJsonAsync ("api/employee", user);
            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();

            return response!;
        }
    }
}
