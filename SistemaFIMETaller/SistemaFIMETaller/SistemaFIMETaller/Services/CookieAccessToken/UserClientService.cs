using Newtonsoft.Json;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Services.CookieAccessToken
{
    public class UserClientService
    {
        private readonly APIService apiService;

        public UserClientService(APIService apiService)
        {
            this.apiService = apiService;
        }

        public async Task<List<Usuario>> ObtenerTodos()
        {
            var response = await apiService.GetAsync("usuarios");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Usuario>>(json);
            }
            return new List<Usuario>();
        }

        public async Task<bool> Actualizar(Usuario usuario)
        {
            var response = await apiService.PutAsync($"usuarios/{usuario.UsuarioId}", usuario);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Eliminar(int id)
        {
            var response = await apiService.DeleteAsync($"usuarios/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Usuario?> ObtenerPorId(int id)
        {
            var response = await apiService.GetAsync($"usuarios/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Usuario>(json);
            }
            return null;
        }

        public async Task<bool> Registrar(Usuario usuario)
        {
            var response = await apiService.PostDataAsync("usuarios", usuario);
            return response.IsSuccessStatusCode;
        }
    }
}