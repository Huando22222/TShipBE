using System.ComponentModel.DataAnnotations;

namespace TShip.Models.Entities
{
    public class Customer
    {
        [Key]
        public Guid Id { get; set; } // Dùng chung với User.Id
        public Guid AccountId { get; set; }
        public User User { get; set; } = null!;
    }

}
