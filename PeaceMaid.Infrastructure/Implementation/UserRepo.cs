using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;

namespace PeaceMaid.Infrastructure.Implementation
{
    public class UserRepo(AppDbContext context) : IUser
    {
        private readonly AppDbContext _context = context;

        Task<ServiceResponse> IUser.AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse> IUser.DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        Task<List<User>> IUser.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<User> IUser.GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        Task<ServiceResponse> IUser.UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
