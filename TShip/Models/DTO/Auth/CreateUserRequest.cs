namespace TShip.Models.DTO.Auth
{
    public class CreateUserRequest
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? IdentityNumber { get; set; }
    }
}
