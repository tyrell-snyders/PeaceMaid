using System.ComponentModel.DataAnnotations;

namespace PeaceMaid.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        required public string Username { get; set; }
        [EmailAddress]
        required public string Email { get; set; }
        required public string HashedPass { get; set; }

        // Optional
        public Location? Address { get; set; }

        // Navigation Properties
        public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
        public ICollection<Review>? Reviews { get; set; } = new List<Review>();
    }
}
