using PeaceMaid.Domain.Entities;
using System.Text.Json.Serialization;

namespace PeaceMaid.Application.DTOs
{
    public class ServiceProviderDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        required public string ServiceDescription { get; set; }
        public decimal Rating { get; set; }
        required public string Availability { get; set; }
        required public int UserId { get; set; }
        required public byte[]? ProfilePicture { get; set; }
        public Location? Address { get; set; }
    }
}
