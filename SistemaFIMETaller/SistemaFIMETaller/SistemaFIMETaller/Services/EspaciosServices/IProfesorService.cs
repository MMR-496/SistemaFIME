using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services.EspaciosServices
{
    public interface IProfesorService
    {
        Task<List<Profesor>> ObtenerTodos();
        Task<Profesor?> ObtenerPorId(int id);
        Task Agregar(Profesor profesor);
        Task Actualizar(Profesor profesor);
        Task Eliminar(int id);

        Task<Profesor?> ObtenerPorCuenta(string cuenta);
    }
}
