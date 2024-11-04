namespace PeaceMaid.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        required public string Username { get; set; }
        required public string Email { get; set; }
        required public string HashedPass { get; set; }
        public Location? Address { get; set; }
    }
}
