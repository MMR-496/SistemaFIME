using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public interface IUserService
    {
        Task<Usuario?> ObtenerPorCuenta(string numeroCuenta);
        Task<List<Usuario>> ObtenerTodos();
        Task<Usuario?> ObtenerPorId(int id);
        Task<bool> Actualizar(Usuario usuario);
        Task<bool> Eliminar(int id);
        Task<bool> RegisterUser(string numeroCuenta, string nombreUsuario, string contrasena, string correoInsti, string rol);
    }
}
