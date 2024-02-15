using System.Text.Json.Serialization;

namespace Ploomes.API.DTO
{
    public class AuthenticatedUserDTO
    {
        public AuthenticatedUserDTO()
        {

        }
        public AuthenticatedUserDTO(string? userName, string? nome, string? role, string? token, DateTime? tokenExpiration, bool? primeiroLogin)
        {
            UserName = userName;
            Nome = nome;
            Role = role;
            Token = token;
            TokenExpiration = tokenExpiration;
            PrimeiroLogin = primeiroLogin;
        }

        public AuthenticatedUserDTO(AuthenticatedUserDTO user)
            : this(user.UserName, user.Nome, user.Role, user.Token, user.TokenExpiration, user.PrimeiroLogin)
        {

        }

        [JsonPropertyName("username")]
        public string? UserName { get; set; }

        [JsonPropertyName("nome")]
        public string? Nome { get; set; }

        [JsonPropertyName("role")]
        public string? Role { get; set; }

        [JsonPropertyName("token")]
        public string? Token { get; set; }

        [JsonPropertyName("token_expiration_date")]
        public DateTime? TokenExpiration { get; set; }

        [JsonPropertyName("primeiro_login")]
        public bool? PrimeiroLogin { get; set; }
    }
}
