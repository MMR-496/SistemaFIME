using System.ComponentModel.DataAnnotations;

namespace SistemaFIMETaller.Models
{
    public class PrestamoMaterial
    {
        public int Id { get; set; }

        public int PrestamoId { get; set; }
        public Prestamo Prestamo { get; set; } = null!;

        public int MaterialId { get; set; }
        public Material Material { get; set; } = null!;
    }
}

