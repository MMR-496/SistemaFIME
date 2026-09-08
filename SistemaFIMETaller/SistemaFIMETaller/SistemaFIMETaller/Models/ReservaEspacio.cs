namespace SistemaFIMETaller.Models
{
    //entidad media para relación muchos a muchos entre Alumnos, Profesores y Espacios
    public class ReservaEspacio
    {
        public int ReservaId { get; set; }
  
        // Relación con Espacio
        public int EspacioId { get; set; }
        public Espacio? Espacio { get; set; }
        // Relación con Alumno
        public int? AlumnoId { get; set; }
        public Alumno? Alumno { get; set; }

        // Relación con Profesor
        public int? ProfesorId { get; set; }
        public Profesor? Profesor { get; set; }

        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }
    }
}
