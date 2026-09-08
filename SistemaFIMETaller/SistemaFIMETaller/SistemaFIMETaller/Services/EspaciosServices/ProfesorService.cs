using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services.EspaciosServices
{
    public class ProfesorService : IProfesorService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public ProfesorService(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task<List<Profesor>> ObtenerTodos()
        {
            await using var db = _context.CreateDbContext();
            return await db.Profesores.ToListAsync();
        }

        public async Task<Profesor?> ObtenerPorId(int id)
        {
            await using var db = _context.CreateDbContext();
            return await db.Profesores.FindAsync(id);
        }

        public async Task Agregar(Profesor profesor)
        {
            await using var db = _context.CreateDbContext();
            db.Profesores.Add(profesor);
            await db.SaveChangesAsync();
        }

        public async Task Actualizar(Profesor profesor)
        {
            await using var db = _context.CreateDbContext();
            db.Profesores.Update(profesor);
            await db.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            await using var db = _context.CreateDbContext();
            var profesor = await db.Profesores.FindAsync(id);
            if (profesor != null)
            {
                db.Profesores.Remove(profesor);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Profesor?> ObtenerPorCuenta(string cuenta)
        {
            await using var db = _context.CreateDbContext();
            return await db.Profesores.FirstOrDefaultAsync(a => a.NumeroCuentaProfesor == cuenta);
        }
    }
}
