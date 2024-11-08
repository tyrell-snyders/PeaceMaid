﻿using Microsoft.EntityFrameworkCore;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Application.Interfaces.Authentication;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;

namespace PeaceMaid.Infrastructure.Implementation
{
    public class UserRepo(AppDbContext context, IPasswordHasher passwordHasher) : IUser
    {
        private readonly AppDbContext _context = context;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        async Task<ServiceResponse> IUser.AddAsync(User user)
        {
            var newUser = new User
            {
                Email = user.Email,
                HashedPass = _passwordHasher.Hash(user.Email),
                Username = user.Username,
                Address = user.Address,
                Bookings = user.Bookings,
                Id = user.Id,
                Reviews = user.Reviews,
            };

            await _context.Users.AddAsync(newUser);
            await SaveChangesAsync();

            return new ServiceResponse(true, "Added");
        }

        async Task<ServiceResponse> IUser.DeleteAsync(int Id)
        {
            var user = await _context.Users.FindAsync(Id);
            if (user == null)
                return new ServiceResponse(false, "User not found");

            _context.Users.Remove(user);
            await SaveChangesAsync();

            return new ServiceResponse(true, "Deleted");
        }

        async Task<List<User>> IUser.GetAllAsync() => await _context.Users.AsNoTracking().ToListAsync();

        async Task<User> IUser.GetByIdAsync(int Id) => await _context.Users.FindAsync(Id);

        async Task <ServiceResponse> IUser.UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await SaveChangesAsync();

            return new ServiceResponse(true, "Updated");
        }

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
