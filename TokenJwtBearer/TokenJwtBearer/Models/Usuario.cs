using System;
using System.ComponentModel.DataAnnotations;

namespace TokenJwtBearer.Models
{
    public class Usuario
    {
        public string Chave { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Token { get; set; }

        public string Role { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }
    }
}
