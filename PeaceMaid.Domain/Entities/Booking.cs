using System.Text.Json.Serialization;

namespace PeaceMaid.Domain.Entities
{
    public class Booking
    {
        public int BookingId { get; set; }

        // User Foreign Key
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }

        // Service Foreign Key
        public int ServiceId { get; set; }
        [JsonIgnore]
        public Service? Service { get; set; }

        // ServiceProvider Foreign Key
        public int ServiceProviderId { get; set; }
        [JsonIgnore]
        public ServiceProvider? ServiceProvider { get; set; }

        public DateTime BookingDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalCost { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation Properties
        [JsonIgnore]
        public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
