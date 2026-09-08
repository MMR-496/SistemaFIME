using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services.EspaciosServices
{
    public class EspacioService : IEspacioService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public EspacioService(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task<List<Espacio>> ObtenerTodos()
        {
            await using var db = _context.CreateDbContext();
            return await db.Espacios.ToListAsync();
        }

        public async Task<Espacio?> ObtenerPorId(int id)
        {
            await using var db = _context.CreateDbContext();
            return await db.Espacios.FindAsync(id);
        }

        public async Task Agregar(Espacio espacio)
        {
            await using var db = _context.CreateDbContext();
            db.Espacios.Add(espacio);
            await db.SaveChangesAsync();
        }

        public async Task Actualizar(Espacio espacio)
        {
            await using var db = _context.CreateDbContext();
            db.Espacios.Update(espacio);
            await db.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            await using var db = _context.CreateDbContext();
            var espacio = await db.Espacios.FindAsync(id);
            if (espacio != null)
            {
                db.Espacios.Remove(espacio);
                await db.SaveChangesAsync();
            }
        }
    }
}