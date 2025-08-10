using System.ComponentModel.DataAnnotations;

namespace TShip.Models.Entities
{
    public class Driver
    {
        [Key]
        public Guid Id { get; set; } 
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public string VehicleInfo { get; set; } = null!;
    }
}
