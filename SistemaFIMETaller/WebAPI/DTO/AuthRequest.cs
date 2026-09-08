using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO
{
    public class AuthRequest
    {
        [Required]
        public string NumeroCuenta { get; set; }
        [Required]
        public string Contrasena { get; set; }

    }
}
