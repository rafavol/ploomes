﻿namespace Ploomes.API.Models
{
    public class User : BaseModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string? NomeCompleto { get; set; }
        public string Salt { get; set; }
        public bool PrimeiroLogin { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
