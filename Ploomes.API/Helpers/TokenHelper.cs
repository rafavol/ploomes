using Microsoft.IdentityModel.Tokens;
using Ploomes.API.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Ploomes.API.Helpers
{
    public static class TokenHelper
    {

        public static void GenerateToken(this AuthenticatedUserDTO authenticatedUserDTO)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            string jwtAPIKEY = Environment.GetEnvironmentVariable("JWT_API_KEY").ToString();
            var key = Encoding.UTF8.GetBytes(jwtAPIKEY);
            var expiration = DateTime.Now.AddHours(12);

            var rolesClaims = new List<Claim> { 
                new Claim(ClaimTypes.Role, authenticatedUserDTO.Role),
                new Claim(ClaimTypes.Name, authenticatedUserDTO.Nome),
            };
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(rolesClaims),
                Expires = expiration,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = "ploomes",
                Issuer = "Ploomes.API"
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            authenticatedUserDTO.Token = tokenHandler.WriteToken(token);
            authenticatedUserDTO.TokenExpiration = expiration;
        }
    }
}
