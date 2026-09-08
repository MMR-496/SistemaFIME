using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly ApplicationDbContext _context;

        public RefreshTokenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> InsertRefreshToken(RefreshToken refreshToken, string numeroCuenta)
        {
            refreshToken.NumeroCuenta = numeroCuenta;
            _context.RefreshTokens.Add(refreshToken);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DisabledUserTokenByNumeroCuenta(string numeroCuenta)
        {
            var tokens = await _context.RefreshTokens
                .Where(rt => rt.NumeroCuenta == numeroCuenta)
                .ToListAsync();

            tokens.ForEach(rt => rt.Enabled = false);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DisableUserToken(string token)
        {
            var refreshToken = await _context.RefreshTokens
                .FirstOrDefaultAsync(rt => rt.Token == token);

            if (refreshToken == null) return false;

            refreshToken.Enabled = false;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> IsRefreshTokenValid(string token)
        {
            return await _context.RefreshTokens
                .AnyAsync(rt => rt.Token == token
                    && rt.Enabled == true
                    && rt.Expires >= DateTime.Now);
        }

        public async Task<Usuario?> FindUserByToken(string token)
        {
            //var refreshToken = await _context.RefreshTokens
            //    .Include(rt => rt.Usuario)
            //    .FirstOrDefaultAsync(rt => rt.Token == token);

            //return refreshToken?.Usuario;
            return await _context.RefreshTokens
            .Where(rt => rt.Token == token)
            .Select(rt => rt.Usuario)
            .FirstOrDefaultAsync();
        
        }
    }
}