using Microsoft.EntityFrameworkCore;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Infrastructure.Data
{
    public class AppDbContext :  DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tells EF core that it owns the Address property and it doesn't need a seperate DBTable for it
            modelBuilder.Entity<User>()
                .OwnsOne(a => a.Address);
        }
    }
}