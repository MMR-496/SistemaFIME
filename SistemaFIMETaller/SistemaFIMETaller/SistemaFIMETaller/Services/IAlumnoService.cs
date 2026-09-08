using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;


namespace SistemaFIMETaller.Services
{
    public interface IAlumnoService
    {
        Task<List<Alumno>> ObtenerTodos();
        Task<Alumno?> ObtenerPorId(int id);
        Task Agregar(Alumno alumno);
        Task Actualizar(Alumno alumno);
        Task Eliminar(int id);

        Task<Alumno?> ObtenerPorCuenta(string cuenta);
    }
}

