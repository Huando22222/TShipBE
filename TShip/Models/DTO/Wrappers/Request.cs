namespace TShip.Models.DTO.Wrappers
{
    public class Request<T>
    {
        public required MetaData Meta { get; set; }
        public required T Data { get; set; }
    }
}
