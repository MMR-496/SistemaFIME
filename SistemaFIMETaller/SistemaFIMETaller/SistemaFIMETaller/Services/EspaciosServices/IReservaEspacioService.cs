using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services.EspaciosServices
{
    public interface IReservaEspacioService
    {
        Task<List<Espacio>> GetEspacios();
        //Task<List<ReservaEspacio>> ObtenerPorReserva(int ReservaId);
        //ctrl + k + u para descomentar, ctrl + k + c para comentar
        Task<List<ReservaEspacio>> ObtenerReservas();

        Task<bool> HorarioDisponible(int espacioId, DateTime fecha, TimeSpan inicio, TimeSpan fin, int? reservaId = null);
        Task Agregar(ReservaEspacio re);
        Task Eliminar(int id);
        Task Actualizar(ReservaEspacio re);
        Task<ReservaEspacio?> ObtenerPorId(int id);
    }
}
