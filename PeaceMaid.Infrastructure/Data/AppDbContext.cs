using Microsoft.EntityFrameworkCore;
using PeaceMaid.Domain.Entities;
using PeaceMaid.Infrastructure.Configs;

namespace PeaceMaid.Infrastructure.Data
{
    public class AppDbContext :  DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Service> Services { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ServiceProviderConfiguration());
            modelBuilder.ApplyConfiguration(new BookingConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new ReviewConfiguration());

            // Tells EF core that it owns the Address property and it doesn't need a seperate DBTable for it
            modelBuilder.Entity<User>()
                .OwnsOne(a => a.Address);
        }
    }
}