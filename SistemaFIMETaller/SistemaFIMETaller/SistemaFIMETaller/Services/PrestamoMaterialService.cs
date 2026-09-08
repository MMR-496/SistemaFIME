using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public class PrestamoMaterialService : IPrestamoMaterialService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public PrestamoMaterialService(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task<List<PrestamoMaterial>> ObtenerPorPrestamo(int prestamoId)
        {
            await using var db = _context.CreateDbContext();
            return await db.PrestamoMateriales
                .Include(pm => pm.Material)
                .Where(pm => pm.PrestamoId == prestamoId)
                .ToListAsync();
        }

        public async Task Agregar(PrestamoMaterial pm)
        {
            await using var db = _context.CreateDbContext();
            db.PrestamoMateriales.Add(pm);
            await db.SaveChangesAsync();
        }

        public async Task Eliminar(PrestamoMaterial pm)
        {
            await using var db = _context.CreateDbContext();
            db.PrestamoMateriales.Remove(pm);
            await db.SaveChangesAsync();
        }
    }
}
