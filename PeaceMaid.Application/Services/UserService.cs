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
            var data = await _httpClient.PostAsJsonAsync("api/User/register", user);
            if (!data.IsSuccessStatusCode)
                return new ServiceResponse(false, $"Request failed with status code: {data.StatusCode}");

            if (data.Content.Headers.ContentType?.MediaType != "application/json" || data.Content.Headers.ContentLength == 0)
            {
                return new ServiceResponse(false, "Server returned an empty or non-JSON response.");
            }

            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();
            if (response == null)
            {
                return new ServiceResponse(false, "Failed to parse server response.");
            }

            return response;
        }

        public async Task<ServiceResponse> DeleteAsync(int Id)
        {
            var data = await _httpClient.DeleteAsync($"api/User/{Id}");
            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();

            return response!;
        }

        public async Task<List<User>> GetAllAsync() => 
            await _httpClient.GetFromJsonAsync<List<User>>("api/User")!;

        public async Task<User> GetByIdAsync(int Id) =>
            await _httpClient.GetFromJsonAsync<User>($"api/User/{Id}")!;

        public async Task<string> LoginAsync(UserDTO user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/login", user);
            if (!response.IsSuccessStatusCode)
            {
                return $"Login failed: {response.ReasonPhrase}";
            }

            var token = await response.Content.ReadAsStringAsync();
            return token ?? "Invalid login response.";
        }

        public async Task<ServiceResponse> UpdateAsync(User user)
        {
            var data = await _httpClient.PutAsJsonAsync ("api/User", user);
            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();

            return response!;
        }
    }
}
