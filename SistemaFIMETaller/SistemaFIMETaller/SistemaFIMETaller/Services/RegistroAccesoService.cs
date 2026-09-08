using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public class RegistroAccesoService : IRegistroAccesoService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public RegistroAccesoService(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task<RegistroAcceso?> ObtenerActivoPorCuenta(string cuenta)
        {
            await using var db = _context.CreateDbContext();
            return await db.RegistrosAcceso
                .FirstOrDefaultAsync(r =>
                    r.NumeroCuenta == cuenta &&
                    r.HoraSalida == null &&
                    r.Fecha.Date == DateTime.Today);
        }

        public async Task RegistrarIngreso(string cuenta, string nombre)
        {
            await using var db = _context.CreateDbContext();
            var registro = new RegistroAcceso
            {
                NumeroCuenta = cuenta,
                NombreCompleto = nombre,
                Fecha = DateTime.Today,
                HoraIngreso = DateTime.Now.TimeOfDay
            };

            db.RegistrosAcceso.Add(registro);
            await db.SaveChangesAsync();
        }

        public async Task RegistrarSalida(string cuenta)
        {
            await using var db = _context.CreateDbContext();
            var registro = await db.RegistrosAcceso
                .FirstOrDefaultAsync(r =>
                    r.NumeroCuenta == cuenta &&
                    r.HoraSalida == null &&
                    r.Fecha.Date == DateTime.Today);

            if (registro != null)
            {
                registro.HoraSalida = DateTime.Now.TimeOfDay;
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<RegistroAcceso>> ObtenerDelDia(DateTime fecha)
        {
            await using var db = _context.CreateDbContext();
            return await db.RegistrosAcceso
                .Where(r => r.Fecha.Date == fecha.Date)
                .OrderByDescending(r => r.HoraIngreso)
                .ToListAsync();
        }

        public async Task<List<RegistroAcceso>> ObtenerTodos()
        {
            await using var db = _context.CreateDbContext();
            return await db.RegistrosAcceso
                .OrderByDescending(r => r.Fecha)
                .ThenByDescending(r => r.HoraIngreso)
                .ToListAsync();
        }
    }
}