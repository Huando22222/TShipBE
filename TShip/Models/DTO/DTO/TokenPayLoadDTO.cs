using TShip.Models.Enums;

namespace TShip.Models.DTO.DTO
{
    public class TokenPayLoadDTO
    {
        public required Guid AccountId { get; set; }
        public required Guid UserId { get; set; }
        public required String Username { get; set; }
        public required List<String> Roles { get; set; }
    }
}
