using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public AlumnoService(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task<List<Alumno>> ObtenerTodos()
        {
            await using var db = _context.CreateDbContext();
            return await db.Alumnos.ToListAsync();
        }

        public async Task<Alumno?> ObtenerPorId(int id)
        {
            await using var db = _context.CreateDbContext();
            return await db.Alumnos.FindAsync(id);
        }

        public async Task Agregar(Alumno alumno)
        {
            await using var db = _context.CreateDbContext();
            db.Alumnos.Add(alumno);
            await db.SaveChangesAsync();
        }

        public async Task Actualizar(Alumno alumno)
        {
            await using var db = _context.CreateDbContext();
            db.Alumnos.Update(alumno);
            await db.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            await using var db = _context.CreateDbContext();
            var alumno = await db.Alumnos.FindAsync(id);
            if (alumno != null)
            {
                db.Alumnos.Remove(alumno);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Alumno?> ObtenerPorCuenta(string cuenta)
        {
            await using var db = _context.CreateDbContext();
            return await db.Alumnos.FirstOrDefaultAsync(a => a.NumeroCuenta == cuenta);
        }
    }
}