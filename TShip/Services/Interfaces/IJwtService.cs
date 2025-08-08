namespace TShip.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string username);
    }
}
