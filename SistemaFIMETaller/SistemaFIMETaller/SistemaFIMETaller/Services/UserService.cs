using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Data;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObtenerPorCuenta(string numeroCuenta) =>
            await _context.Usuarios
                .FirstOrDefaultAsync(u => u.NumeroCuenta == numeroCuenta);

        public async Task<bool> RegisterUser(string nombreUsuario, string numeroCuenta, string contrasena, string correoInsti, string rol)
        {
            //Si se encuentra con un usuario con el mismo número de cuenta o correo electronico...
            var accountCount = await _context.Usuarios
                .CountAsync(u => u.NumeroCuenta == numeroCuenta || u.CorreoInsti == correoInsti);
            //No ejecuta nada
            if (accountCount > 0) return false;
            //De lo contrario añade al nuevo usuario
            var usuario = new Usuario
            {
                NombreUsuario = nombreUsuario,
                NumeroCuenta = numeroCuenta,
                Contrasena = contrasena,
                CorreoInsti = correoInsti,
                Rol = rol
            };

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Usuario>> ObtenerTodos() =>
            await _context.Usuarios.ToListAsync();

        public async Task<Usuario?> ObtenerPorId(int id) =>
            await _context.Usuarios.FindAsync(id);

        public async Task<bool> Actualizar(Usuario usuario)
        {
            var existente = await _context.Usuarios.FindAsync(usuario.UsuarioId);
            if (existente == null) return false;

            existente.NombreUsuario = usuario.NombreUsuario;
            existente.NumeroCuenta = usuario.NumeroCuenta;
            existente.CorreoInsti = usuario.CorreoInsti;
            existente.Rol = usuario.Rol;

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;
            _context.Usuarios.Remove(usuario);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}