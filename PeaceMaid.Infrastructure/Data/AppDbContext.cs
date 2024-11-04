using Microsoft.EntityFrameworkCore;

namespace PeaceMaid.Infrastructure.Data
{
    public class AppDbContext :  DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}