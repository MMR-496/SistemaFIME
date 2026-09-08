using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaFIMETaller.Services;
using WebAPI.DTO;
using WebAPI.Infrastructure;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService dataAccess;
        private readonly IRefreshTokenService refreshTokenData;
        private readonly TokenProvider tokenProvider;

        public AuthController(IUserService dataAccess, TokenProvider tokenProvider, IRefreshTokenService refreshTokenData)
        {
            this.dataAccess = dataAccess;
            this.tokenProvider = tokenProvider;
            this.refreshTokenData = refreshTokenData;
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest request)
        {
            try
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Contrasena);
                var result = await dataAccess.RegisterUser(request.NombreUsuario, request.NumeroCuenta, hashedPassword, request.CorreoInsti, request.Rol);

                if (result)
                    return Ok();
                else
                    return BadRequest("El usuario ya existe");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost] 
        public async Task<ActionResult<AuthResponse>> Login(AuthRequest request) //Si no lo hacemos un task asíncrono dará problemas
        {
            AuthResponse response = new AuthResponse();
            var user = await dataAccess.ObtenerPorCuenta(request.NumeroCuenta);
            
            if(user == null) return BadRequest("Datos inválidos");

            var verifyPassword = BCrypt.Net.BCrypt.Verify(request.Contrasena, user.Contrasena); //Se compara la contraseña hasheada con la contraseña en string
            if (!verifyPassword) return BadRequest("Datos inválidos");

            //Codigo para la generacion de token de acceso
            var token = tokenProvider.GenerateToken(user);
            response.AccessToken = token.AccessToken;

            //Codigo para la generación de token refrescado
            response.RefreshToken = token.RefreshToken.Token;
            await refreshTokenData.DisabledUserTokenByNumeroCuenta(request.NumeroCuenta);
            await refreshTokenData.InsertRefreshToken(token.RefreshToken, request.NumeroCuenta);

            return Ok(response);

        }

        [HttpPost("refresh")]
        public async Task<ActionResult<AuthResponse>> RefreshToken()
        {
            AuthResponse response = new AuthResponse();
            
            var refreshToken = Request.Cookies["refreshtoken"];
            if (string.IsNullOrEmpty(refreshToken)) return BadRequest();

            var isValid = await refreshTokenData.IsRefreshTokenValid(refreshToken);
            if (!isValid) return BadRequest();

            var currentUser = await refreshTokenData.FindUserByToken(refreshToken);
            if (currentUser == null) return BadRequest();

            var token = tokenProvider.GenerateToken(currentUser);
            response.AccessToken = token.AccessToken;
            response.RefreshToken = token.RefreshToken.Token;

            await refreshTokenData.DisableUserToken(refreshToken);
            await refreshTokenData.InsertRefreshToken(token.RefreshToken, currentUser.NumeroCuenta);
            return Ok(response);
        }

        [HttpPost("logout")]
        public ActionResult Logout()
        {
            var refreshToken = Request.Cookies["refreshtoken"];
            if(refreshToken != null) refreshTokenData.DisableUserToken(refreshToken);
            
            return Ok();
             
        }


    }

}
