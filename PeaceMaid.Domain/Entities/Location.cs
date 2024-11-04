namespace PeaceMaid.Domain.Entities
{
    public class Location
    {
        public string? Country { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public int PostalCode { get; set; }
        public string? HouseAddress { get; set; }
    }
}
