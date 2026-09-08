using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public interface IPrestamoMaterialService
    {
        Task<List<PrestamoMaterial>> ObtenerPorPrestamo(int prestamoId);
        Task Agregar(PrestamoMaterial pm);
        Task Eliminar(PrestamoMaterial pm);
    }
}
