using System.ComponentModel.DataAnnotations;

namespace TShip.Models.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }

}
