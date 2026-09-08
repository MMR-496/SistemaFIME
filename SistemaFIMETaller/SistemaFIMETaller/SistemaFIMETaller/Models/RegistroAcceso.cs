using System.ComponentModel.DataAnnotations;

namespace SistemaFIMETaller.Models
{
    public class RegistroAcceso
    {
        public int RegistroAccesoId { get; set; }

        [Required]
        public string NumeroCuenta { get; set; }

        [Required]
        public string NombreCompleto { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        public TimeSpan? HoraIngreso { get; set; }
        public TimeSpan? HoraSalida { get; set; }
    }
}

