using System.ComponentModel.DataAnnotations;

namespace PeaceMaid.Application.DTOs
{
    public class UserDTO
    {
        [EmailAddress]
        required public string Email { get; set; }
        required public string Password { get; set; }
    }
}
