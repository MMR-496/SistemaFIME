using System.ComponentModel.DataAnnotations;

namespace SistemaFIMETaller.Models
{
    public class Usuario
    {
        public int UsuarioId { get; set; }

         
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string NombreUsuario { get; set; }


        
        [Required(ErrorMessage = "La matrícula es obligatoria.")]
        public string NumeroCuenta { get; set; }


        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [MinLength(3, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]

        public string Contrasena { get; set; }
        
        [Required(ErrorMessage = "El correo es obligatorio.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string CorreoInsti { get; set; }

        [Required(ErrorMessage = "Selecciona un rol.")]
        public string Rol { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }

    }
}
