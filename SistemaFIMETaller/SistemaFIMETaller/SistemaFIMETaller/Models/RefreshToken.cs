using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SistemaFIMETaller.Models
{
    public class RefreshToken
    {
        [Key]
        public string Token { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime Expires { get; set; }
        public bool Enabled { get; set; }

        [ForeignKey("Usuario")]
        public string NumeroCuenta { get; set; }
        public Usuario Usuario { get; set; }
    }
}