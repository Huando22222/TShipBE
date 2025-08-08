using System.ComponentModel.DataAnnotations;

namespace TShip.Models.Entities
{
    public class Driver
    {
        [Key]
        public Guid Id { get; set; } // Dùng chung với User.Id
        public Guid AccountId { get; set; }
        public User User { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public string VehicleInfo { get; set; } = null!;
    }

}
