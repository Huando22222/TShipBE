using System.Data;
using TShip.Models.Enums;

namespace TShip.Models.Entities
{
    public class AccountRole
    {
        public Guid AccountId { get; set; }
        public Account Account { get; set; } = null!;

        public Role Role { get; set; }  // Enum
    }
}
