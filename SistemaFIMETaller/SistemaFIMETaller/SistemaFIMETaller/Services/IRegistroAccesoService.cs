using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public interface IRegistroAccesoService
    {
        Task<RegistroAcceso?> ObtenerActivoPorCuenta(string cuenta);
        Task RegistrarIngreso(string cuenta, string nombre);
        Task RegistrarSalida(string cuenta);
        Task<List<RegistroAcceso>> ObtenerDelDia(DateTime fecha);
        Task<List<RegistroAcceso>> ObtenerTodos();

    }
}

