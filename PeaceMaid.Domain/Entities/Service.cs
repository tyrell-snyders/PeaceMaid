namespace PeaceMaid.Domain.Entities
{
    public class Service
    {
        public int ServiceId { get; set; }
        public int ServiceIdProvider { get; set; }
        required public string Name { get; set; }
        required public decimal Price { get; set; }
        public TimeSpan Duration { get; set; }
        public string? Description { get; set; }
    }
}
