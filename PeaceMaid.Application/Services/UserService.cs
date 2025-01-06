using Microsoft.JSInterop;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;
using System.Net.Http.Json;
using System.Text.Json;

namespace PeaceMaid.Application.Services
{
    public class UserService(HttpClient httpClient, IJSRuntime jSRuntime) : IUserService
    {
        private readonly HttpClient _httpClient = httpClient;
        private readonly IJSRuntime _jsRuntime = jSRuntime;

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

        public async Task<bool> IsSessionValid()
        {
            var expiry = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "authExpiry");

            if (!string.IsNullOrEmpty(expiry) && DateTime.TryParse(expiry, out var expiryTime))
            {
                if (DateTime.UtcNow <= expiryTime)
                    return true; // Session is still valid
            }

            // Clear the session
            await Logout();
            return false;
        }

        public async Task<LoginResponse> LoginAsync(UserDTO user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/login", user);

            if (!response.IsSuccessStatusCode)
            {
                return new LoginResponse(false, $"Login failed: {response.ReasonPhrase}", string.Empty);
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);

            if (responseData != null && responseData.TryGetValue("token", out var token))
            {
                // Store token, username, and userID in sessionStorage
                var userId = responseData.TryGetValue("userID", out var userIdObj) ? userIdObj.ToString() : string.Empty;
                var username = responseData.TryGetValue("username", out var usernameObj) ? usernameObj.ToString() : string.Empty;

                await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "authToken", token.ToString());
                await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "userID", userId);
                await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "username", username);
                await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "authExpiry", DateTime.Now.AddHours(168).ToString());

                return new LoginResponse(true, "Login Successful", token.ToString());
            }

            return new LoginResponse(false, "Invalid login response.", string.Empty);
        }

        public async Task Logout()
        {
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "authToken");
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "userID");
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "username");
            await _jsRuntime.InvokeVoidAsync("sessionStorage.removeItem", "authExpiry");
        }

        public async Task<ServiceResponse> UpdateAsync(User user)
        {
            var data = await _httpClient.PutAsJsonAsync ("api/User", user);
            var response = await data.Content.ReadFromJsonAsync<ServiceResponse>();

            return response!;
        }
    }
}
