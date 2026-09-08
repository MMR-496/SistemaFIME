using System.ComponentModel.DataAnnotations;

namespace SistemaFIMETaller.Models
{
    public class Alumno
    {
        public int AlumnoId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "La matrícula es obligatoria.")]
        public string NumeroCuenta { get; set; }

        [Required(ErrorMessage = "El grupo es obligatorio.")]
        public string Grupo { get; set; }

        virtual public ICollection<ReservaEspacio>? Reservas { get; set; }
    }
}
