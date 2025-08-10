using System.ComponentModel.DataAnnotations;

namespace TShip.Models.Entities
{
    public class Store
    {
        [Key]
        public Guid Id { get; set; } 
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public string StoreName { get; set; } = null!;
        public string StoreAddress { get; set; } = null!;
    }

}
