using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SistemaFIMETaller.Models;
using SistemaFIMETaller.Services;
using WebAPI.DTOs;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IUserService dataAccess;

        public UsuariosController(IUserService dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> ObtenerTodos()
        {
            var usuarios = await dataAccess.ObtenerTodos();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ObtenerPorId(int id)
        {
            var usuario = await dataAccess.ObtenerPorId(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }



        [HttpPut("{id}")]
        public async Task<ActionResult> Actualizar(int id, UpdateUserRequest request)
        {
            try
            {
                var usuario = new Usuario
                {
                    UsuarioId = id,
                    NombreUsuario = request.NombreUsuario,
                    NumeroCuenta = request.NumeroCuenta,
                    CorreoInsti = request.CorreoInsti,
                    Rol = request.Rol
                };
                var result = await dataAccess.Actualizar(usuario);
                if (result) return Ok();
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Eliminar(int id)
        {
            var result = await dataAccess.Eliminar(id);
            if (result) return Ok();
            return NotFound();
        }
    }
}