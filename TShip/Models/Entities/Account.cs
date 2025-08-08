using System.ComponentModel.DataAnnotations;

namespace TShip.Models.Entities
{
    public class Account
    {
        [Key]
        public Guid Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime LastLogin { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        // Bảng phụ Role nếu nhiều roles
        public ICollection<AccountRole> AccountRoles { get; set; } = new List<AccountRole>();
        public  string? Token { get; set; }
        public  string? RefreshToken { get; set; }
        // FK
        public Guid UserId { get; set; }
        
    }

}
