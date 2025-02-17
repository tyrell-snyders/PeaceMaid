﻿using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application.Interfaces
{
    public interface IUser
    {
        Task<ServiceResponse> AddAsync(User user);
        Task<ServiceResponse> UpdateAsync(User user);
        Task<ServiceResponse> DeleteAsync(int Id);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(int Id);
        Task<DataResponse> LoginAsync(UserDTO userDTO);
        Task<User?> GetByEmailAsync(string Email);
    }                                             
}
