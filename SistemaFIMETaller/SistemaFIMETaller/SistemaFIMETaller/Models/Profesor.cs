using System.ComponentModel.DataAnnotations;

namespace SistemaFIMETaller.Models
{
    public class Profesor
    {
        public int ProfesorId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        public string NombreProfesor { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        public string ApellidoProfesor { get; set; }

        [Required(ErrorMessage = "El número de cuenta es obligatorio.")]
        public string NumeroCuentaProfesor { get; set; }

        virtual public ICollection<ReservaEspacio>? Reservas { get; set; }

    }
}
