using System.ComponentModel.DataAnnotations;

namespace SistemaFIMETaller.Models
{
    public class Material
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Numero { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        public string? Marca { get; set; }
        public string? Modelo { get; set; }
        public string? Serie { get; set; }

        [Required]
        [MaxLength(150)]
        public string Ubicacion { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Custodio { get; set; } = string.Empty;

        public string? Observaciones { get; set; }
    }
}
