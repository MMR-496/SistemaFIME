using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services.EspaciosServices
{
    public interface IEspacioService
    {
        Task<List<Espacio>> ObtenerTodos();
        Task<Espacio?> ObtenerPorId(int id);
        Task Agregar(Espacio espacio);
        Task Actualizar(Espacio espacio);
        Task Eliminar(int id);
    }
}
