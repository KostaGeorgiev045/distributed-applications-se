using DigitalLibrary.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DigitalLibrary.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly Dictionary<string, string> _admin = new()
        {
            { "admin", "admin" }
        };

        private readonly string _tokenKey;

        public AuthRepository(string tokenKey)
        {
            _tokenKey = tokenKey;
        }

        public string? Authenticate(string adminId, string secret)
        {
            if (!_admin.Any(x => x.Key == adminId && x.Value == secret))
            {
                return null;
            }

            JwtSecurityTokenHandler handler = new();
            var key = Encoding.ASCII.GetBytes(_tokenKey);
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new(new Claim[]
                {
                    new(ClaimTypes.Name, adminId),
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }
    }
}
