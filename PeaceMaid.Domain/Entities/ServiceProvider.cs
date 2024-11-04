namespace PeaceMaid.Domain.Entities
{
    public class ServiceProvider
    {
        public int Id { get; set; }
        required public string ServiceDescription { get; set; }
        public decimal Rating { get; set; }
        public byte ProfilePicture { get; set; }
        required public string Availability { get; set; }

        // Optional
        public Location? Address { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
