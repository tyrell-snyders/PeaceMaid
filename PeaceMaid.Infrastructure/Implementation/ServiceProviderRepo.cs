﻿using Microsoft.EntityFrameworkCore;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Application.Interfaces;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;

namespace PeaceMaid.Infrastructure.Implementation
{
    public class ServiceProviderRepo(AppDbContext appDbContext) : ISProvider
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task<ServiceResponse> AddAsync(ServiceProvider serviceProvider)
        {
            await _context.ServiceProviders.AddAsync(serviceProvider);
            await SaveChangesAsync();

            return new(true, "Added");
        }

        public async Task<ServiceResponse> DeleteAsync(int Id)
        {
            var serviceProvider = await _context.ServiceProviders.FindAsync(Id);
            if (serviceProvider == null)
                return new(false, "Service Provider not found!");

            _context.ServiceProviders.Remove(serviceProvider);
            await _context.SaveChangesAsync();
            return new(true, "Deleted");
        }

        public async Task<List<ServiceProvider>> GetAsync()
            => await _context.ServiceProviders.Distinct().Include(sp => sp.User).ToListAsync();

        public async Task<ServiceProvider?> GetProviderAsync(int userId)
            => await _context.ServiceProviders.FirstOrDefaultAsync(x => x.UserId == userId);

        public async Task<ServiceResponse> UpdateAsync(ServiceProvider serviceProvider)
        {
            _context.ServiceProviders.Update(serviceProvider);
            await SaveChangesAsync();

            return new(true, "Updated");
        }

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
