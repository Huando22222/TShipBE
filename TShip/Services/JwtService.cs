using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TShip.Configurations;
using TShip.Models.DTO.DTO;
using TShip.Services.Interfaces;

namespace TShip.Services
{
    public class JwtService(IOptions<JwtSettings> jwtSettings) : IJwtService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private readonly SigningCredentials creds = new(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Key)), SecurityAlgorithms.HmacSha256);
       
        public string GenerateToken(TokenPayLoadDTO payload ,double? expiresInDays = null)
        {
            var expDays = expiresInDays ?? _jwtSettings.ExpiresInDays;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, payload.Username),
                new Claim("userId", payload.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, payload.AccountId.ToString())
            };
                payload.Roles.ForEach(role =>
            {
                claims.Add(new Claim("role", role)); 
            });
            var token = new JwtSecurityToken(
                issuer: _jwtSettings .Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(expDays),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string? GenerateRefreshToken(String token)//_jwtSettings.RefreshExpiresInDays
        {
             TokenPayLoadDTO? tokenPayLoadDTO = DecodeToken(token);
            if (tokenPayLoadDTO != null)
            {
                return GenerateToken(tokenPayLoadDTO, _jwtSettings.RefreshExpiresInDays);
            }
            else
            {
                return null;
            }
        
        }

        public TokenPayLoadDTO? DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _jwtSettings.Issuer,

                ValidateAudience = true,
                ValidAudience = _jwtSettings.Audience,

                ValidateLifetime = true, // kiểm tra exp
                ClockSkew = TimeSpan.Zero, // không cho trễ

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key))
            };

            try
            {
                var principal = handler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                var userId = principal.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
                var username = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Sub)?.Value;
                var accountId = principal.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Jti)?.Value;
                var roles = principal.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();

                if (userId == null || username == null || accountId == null)
                    return null;

                return new TokenPayLoadDTO
                {
                    AccountId = Guid.Parse(accountId),
                    UserId = Guid.Parse(userId),
                    Username = username,
                    Roles = roles
                };
            }
            catch (SecurityTokenException)
            {
                return null;
            }
        }


    }
}

