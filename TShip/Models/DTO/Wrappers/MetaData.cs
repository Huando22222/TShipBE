namespace TShip.Models.DTO.Wrappers
{
    public class MetaData
    {
        //public required string Server { get; set; }
        //public required string ClientVersion { get; set; }
        public string Server { get; set; } = string.Empty; //=TShip
        public string ClientVersion { get; set; } = string.Empty;
    }
}
