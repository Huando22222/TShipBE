using TShip.Models.DTO.DTO;

namespace TShip.Services.Interfaces
{
    public interface IJwtService
    {
        string? GenerateToken(TokenPayLoadDTO payload, double? expiresInDays = null);
        string? GenerateRefreshToken(String token);
        TokenPayLoadDTO? DecodeToken(string token);
    }
}
