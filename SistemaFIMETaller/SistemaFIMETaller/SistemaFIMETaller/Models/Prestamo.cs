using System.ComponentModel.DataAnnotations;

namespace SistemaFIMETaller.Models
{
    public class Prestamo
    {
        public int Id { get; set; }

        // Relación con Alumno
        [Required]
        public int AlumnoId { get; set; }
        public Alumno? Alumno { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Now;

        // Relación muchos a muchos con Material
        public List<PrestamoMaterial> PrestamoMateriales { get; set; } = new();

        public string? Observaciones { get; set; }
        public bool Estado { get; set; } = true;
    }
}

