namespace TShip.Models.Entities
{
    public class Account
    {
        public Guid Id { get; set; }
        public required string username { get; set; }
        public required string password { get; set; }
    }
}
