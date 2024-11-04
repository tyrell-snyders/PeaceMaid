namespace PeaceMaid.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        required public string Username { get; set; }
        required public string Email { get; set; }
        required public string HashedPass { get; set; }

        // Optional
        public Location? Address { get; set; }
    }
}
