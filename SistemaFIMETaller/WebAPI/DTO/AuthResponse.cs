namespace WebAPI.DTO
{
    public class AuthResponse
    {
        //Se retorna cuando el usuario inicia sesión correctamente
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
