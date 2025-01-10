using Microsoft.AspNetCore.Http;
using PeaceMaid.Domain.Entities;

namespace PeaceMaid.Application.DTOs
{
    public class ServiceProviderDTO
    {
        required public string ServiceDescription { get; set; }
        public decimal Rating { get; set; }
        required public string Availability { get; set; }
        required public int UserId { get; set; }
        required public IFormFile ProfilePicture { get; set; }
        public Location? Address { get; set; }
    }
}
