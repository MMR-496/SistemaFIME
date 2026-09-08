using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public class PrestamoService : IPrestamoService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public PrestamoService(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task<List<Prestamo>> ObtenerTodos()
        {
            await using var db = _context.CreateDbContext();
            return await db.Prestamos
                .Include(p => p.Alumno)
                .Include(p => p.PrestamoMateriales)
                    .ThenInclude(pm => pm.Material)
                .ToListAsync();
        }

        public async Task<Prestamo?> ObtenerPorId(int id)
        {
            await using var db = _context.CreateDbContext();
            return await db.Prestamos
                .Include(p => p.Alumno)
                .Include(p => p.PrestamoMateriales)
                    .ThenInclude(pm => pm.Material)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Agregar(Prestamo prestamo)
        {
            await using var db = _context.CreateDbContext();
            db.Prestamos.Add(prestamo);
            await db.SaveChangesAsync();
        }

        public async Task Actualizar(Prestamo prestamo)
        {
            await using var db = _context.CreateDbContext();
            db.Prestamos.Update(prestamo);
            await db.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            await using var db = _context.CreateDbContext();
            var prestamo = await db.Prestamos.FindAsync(id);
            if (prestamo != null)
            {
                db.Prestamos.Remove(prestamo);
                await db.SaveChangesAsync();
            }
        }
    }
}
