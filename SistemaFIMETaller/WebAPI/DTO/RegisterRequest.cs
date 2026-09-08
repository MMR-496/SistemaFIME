using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTO
{
    public class RegisterRequest
    {
        [Required]
        public string NombreUsuario { get; set; }
        [Required]
        public string NumeroCuenta { get; set; }
        [Required]
        public string Contrasena { get; set; }
        [Required]
        public string CorreoInsti { get; set; }
        [Required]
        public string Rol { get; set; }
    }
}
