using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public MaterialService(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task<List<Material>> ObtenerTodos()
        {
            await using var db = _context.CreateDbContext();
            return await db.Materiales.ToListAsync();
        }

        public async Task<Material?> ObtenerPorId(int id)
        {
            await using var db = _context.CreateDbContext();
            return await db.Materiales.FindAsync(id);
        }

        public async Task Agregar(Material material)
        {
            await using var db = _context.CreateDbContext();
            db.Materiales.Add(material);
            await db.SaveChangesAsync();
        }

        public async Task Actualizar(Material material)
        {
            await using var db = _context.CreateDbContext();
            db.Materiales.Update(material);
            await db.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            await using var db = _context.CreateDbContext();
            var material = await db.Materiales.FindAsync(id);
            if (material != null)
            {
                db.Materiales.Remove(material);
                await db.SaveChangesAsync();
            }
        }
    }
}