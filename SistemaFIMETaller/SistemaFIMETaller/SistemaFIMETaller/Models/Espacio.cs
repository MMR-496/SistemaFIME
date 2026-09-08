namespace SistemaFIMETaller.Models
{
    public class Espacio
    {
        public int EspacioId{ get; set; }
        public string NombreEspacio { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public string Responsable { get; set; } = string.Empty;
        public string? Observaciones { get; set; }
        virtual public ICollection<ReservaEspacio>? Reservas { get; set; }
    }
}
