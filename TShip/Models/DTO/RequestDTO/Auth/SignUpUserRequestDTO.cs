namespace TShip.Models.DTO.RequestDTO.Auth
{
    public class SignUpUserRequestDTO
    {
        public required Guid AccountId { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string PhoneNumber { get; set; }
        //public string? AvatarUrl { get; set; }
        public IFormFile? Avatar { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? IdentityNumber { get; set; }
    }
}
