using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public interface IRefreshTokenService
    {

        Task<bool> InsertRefreshToken(RefreshToken refreshToken, string numeroCuenta);
        Task<bool> DisabledUserTokenByNumeroCuenta(string numeroCuenta);
        Task<bool> DisableUserToken(string token);
        Task<bool> IsRefreshTokenValid(string token);
        Task<Usuario?> FindUserByToken(string token);
    }
}
