using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SistemaFIMETaller.Models;


namespace WebAPI.Infrastructure
{
    public class TokenProvider
    {
        private readonly IConfiguration configuration;

        public TokenProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Token GenerateToken(Usuario usuario)
        {
            var accessToken = GenerateAccessToken(usuario);
            var refreshToken = GenerateRefreshToken();
            return new Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid().ToString(),
                Expires = DateTime.Now.AddMonths(1),
                CreatedDate = DateTime.Now,
                Enabled = true,
            };
        }


        private string GenerateAccessToken(Usuario usuario)
        {
            string secretKey = configuration["JWT:SecretKey"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity([
                    new Claim(ClaimTypes.Name, usuario.NumeroCuenta),
                    new Claim("NombreUsuario", usuario.NombreUsuario),
                    new Claim(ClaimTypes.Role, usuario.Rol)
                    ]),
                Expires = DateTime.Now.AddSeconds(15), //antes era 1 minuto
                SigningCredentials = credentials,
                Issuer = configuration["JWT:Issuer"],
                Audience = configuration["JWT:Audience"]
            };
            return new JsonWebTokenHandler().CreateToken(tokenDescriptor);
        }
        public class Token
        {
            public string AccessToken { get; set; }
            public RefreshToken RefreshToken { get; set; }
            
            //Refresh Token aquí
        }
    }
}
