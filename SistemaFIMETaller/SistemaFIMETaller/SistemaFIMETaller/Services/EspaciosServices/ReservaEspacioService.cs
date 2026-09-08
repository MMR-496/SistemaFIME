using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services.EspaciosServices
{
    public class ReservaEspacioService : IReservaEspacioService
    {
        private readonly IDbContextFactory<ApplicationDbContext> _context;

        public ReservaEspacioService(IDbContextFactory<ApplicationDbContext> context)
        {
            _context = context;
        }

        public async Task<List<Espacio>> GetEspacios()
        {
            await using var db = _context.CreateDbContext();
            return await db.Espacios.ToListAsync();
        }

        public async Task<List<ReservaEspacio>> ObtenerReservas()
        {
            await using var db = _context.CreateDbContext();
            return await db.ReservaEspacios
                .Include(e => e.Espacio)
                .Include(a => a.Alumno)
                .Include(p => p.Profesor)
                .ToListAsync();
        }

        public async Task<ReservaEspacio?> ObtenerPorId(int id)
        {
            await using var db = _context.CreateDbContext();
            return await db.ReservaEspacios.FindAsync(id);
        }

        public async Task<bool> HorarioDisponible(int espacioId, DateTime fecha, TimeSpan inicio, TimeSpan fin, int? reservaId = null)
        {
            await using var db = _context.CreateDbContext();
            return !await db.ReservaEspacios
                .AnyAsync(r =>
                    r.EspacioId == espacioId &&
                    r.Fecha == fecha &&
                    (reservaId == null || r.ReservaId != reservaId) && //esto es para evitar conflictos con sí mismo, pues podría intentar reducir la cantidad de horas o no hacer nada y marcaría que el horario ya está ocupado
                    inicio < r.HoraFin &&
                    fin > r.HoraInicio
                );
        }

        public async Task Agregar(ReservaEspacio re)
        {
            await using var db = _context.CreateDbContext();
            db.ReservaEspacios.Add(re);
            await db.SaveChangesAsync();
        }

        public async Task Eliminar(int id)
        {
            await using var db = _context.CreateDbContext();
            var reserva = await db.ReservaEspacios.FindAsync(id);
            if (reserva != null)
            {
                db.ReservaEspacios.Remove(reserva);
                await db.SaveChangesAsync();
            }
        }

        public async Task Actualizar(ReservaEspacio re)
        {
            await using var db = _context.CreateDbContext();
            db.ReservaEspacios.Update(re);
            await db.SaveChangesAsync();
        }
    }
}