namespace TShip.Models.DTO.ResponseDTO
{
    public class SignInAccountDTO
    {
        public required Guid Id { get; set; }
        public required string Username { get; set; } 
        public required bool IsActive { get; set; }
        public required DateTime LastLogin { get; set; }
        public required int FailedLoginAttempts { get; set; }
        public required string Token { get; set; } 
        public required string RefreshToken { get; set; } 

        // Không nullable, khởi tạo mặc định rỗng
        public required List<string> Roles { get; set; } = new List<string>();
    }
}
