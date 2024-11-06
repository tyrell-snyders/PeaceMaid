using Microsoft.EntityFrameworkCore;
using PeaceMaid.Application;
using PeaceMaid.Application.DTOs;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Data;

namespace PeaceMaid.Infrastructure.Implementation
{
    public class ServiceRepo(AppDbContext context) : IService
    {
        private readonly AppDbContext _context = context;

        public async Task<ServiceResponse> AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await SaveChangesAsync();

            return new(true, "Added");
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var data = await _context.Services.FindAsync(id);
            if (data != null)
                return new(false, "Could Not find service");

            _context.Services.Remove(data!);

            await SaveChangesAsync();
            return new(true, "Deleted");
        }

        public async Task<List<Service>> GetAsync() => 
            await _context.Services
                .Include(s => s.ServiceProvider)
                .Distinct().ToListAsync();

        public async Task<Service?> GetServiceAsync(int id) => 
            await _context.Services
                .Include(s => s.ServiceProvider)
                .FirstOrDefaultAsync(s => s.ServiceId == id);

        public async Task<ServiceResponse> UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await SaveChangesAsync();

            return new(true, "Added");
        }

        private async Task SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
