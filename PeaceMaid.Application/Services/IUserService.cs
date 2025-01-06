using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application.Services
{
    public interface IUserService
    {
        Task<ServiceResponse> AddAsync(User user);
        Task<ServiceResponse> UpdateAsync(User user);
        Task<ServiceResponse> DeleteAsync(int Id);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int Id);
        Task<LoginResponse> LoginAsync(UserDTO user);
        Task Logout();
        Task<bool> IsSessionValid();
    }
}
