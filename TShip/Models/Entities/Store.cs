using System.ComponentModel.DataAnnotations;

namespace TShip.Models.Entities
{
    public class Store
    {
        [Key]
        public Guid Id { get; set; } // Dùng chung với User.Id
        public Guid AccountId { get; set; }
        public User User { get; set; } = null!;
        public string StoreName { get; set; } = null!;
        public string StoreAddress { get; set; } = null!;
    }

}
