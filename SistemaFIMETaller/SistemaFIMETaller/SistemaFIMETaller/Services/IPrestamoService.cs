using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public interface IPrestamoService
    {
        Task<List<Prestamo>> ObtenerTodos();
        Task<Prestamo?> ObtenerPorId(int id);
        Task Agregar(Prestamo prestamo);
        Task Actualizar(Prestamo prestamo);
        Task Eliminar(int id);
    }
}

