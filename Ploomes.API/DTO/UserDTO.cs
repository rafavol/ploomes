using Ploomes.API.Validators;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static Ploomes.API.Validators.AgeCheckAttribute;

namespace Ploomes.API.DTO
{
        public class UserDTO
        {
        
            [JsonPropertyName("user_id")]
            public long Id { get; set; }

            [Required]
            [JsonPropertyName("username")]
            public string? UserName { get; set; }

            [Required]
            [JsonPropertyName("nome")]
            public string? NomeCompleto { get; set; }

            [Required]
            [JsonPropertyName("senha")]
            public string? Password { get; set; }

            [Required]
            [JsonPropertyName("data_nascimento")]
            [AgeCheck]
            public DateTime DataNascimento { get; set; }
        }
}
