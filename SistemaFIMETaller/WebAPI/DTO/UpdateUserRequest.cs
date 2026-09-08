namespace WebAPI.DTOs
{
    public class UpdateUserRequest
    {
        public string NombreUsuario { get; set; }
        public string NumeroCuenta { get; set; }
        public string CorreoInsti { get; set; }
        public string Rol { get; set; }
    }
}