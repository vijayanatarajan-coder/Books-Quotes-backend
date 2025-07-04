namespace BackendApi.Models
{
    public class UserRegistration
    {
        public int Id { get; set; } 

        public required string Username { get; set; }

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
