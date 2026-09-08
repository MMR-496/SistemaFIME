using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public interface IMaterialService
    {
        Task<List<Material>> ObtenerTodos();
        Task<Material?> ObtenerPorId(int id);
        Task Agregar(Material material);
        Task Actualizar(Material material);
        Task Eliminar(int id);
    }

}
