using Microsoft.EntityFrameworkCore;
using SistemaFIMETaller.Models;

namespace SistemaFIMETaller.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Alumno> Alumnos { get; set; } = null!;
        public DbSet<Material> Materiales { get; set; } = null!;
        public DbSet<Prestamo> Prestamos { get; set; } = null!;
        public DbSet<PrestamoMaterial> PrestamoMateriales { get; set; } = null!;
        public DbSet<RegistroAcceso> RegistrosAcceso { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<Espacio> Espacios { get; set; }
        public DbSet<ReservaEspacio> ReservaEspacios{ get; set; }
        public DbSet<Usuario> Usuarios { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Normalizar NumeroCuenta
            modelBuilder.Entity<Alumno>()
                .Property(a => a.NumeroCuenta)
                .HasColumnType("varchar(20)")
                .HasConversion(
                    v => v.Trim(),
                    v => v.Trim()
                );

            modelBuilder.Entity<Profesor>()
                .Property(a => a.NumeroCuentaProfesor)
                .HasColumnType("varchar(20)")
                .HasConversion(
                    v => v.Trim(),
                    v => v.Trim()
                );

            // Tabla puente PrestamoMaterial
            modelBuilder.Entity<PrestamoMaterial>()
                .HasKey(pm => pm.Id);

            modelBuilder.Entity<PrestamoMaterial>()
                .HasOne(pm => pm.Prestamo)
                .WithMany(p => p.PrestamoMateriales)
                .HasForeignKey(pm => pm.PrestamoId);

            modelBuilder.Entity<PrestamoMaterial>()
                .HasOne(pm => pm.Material)
                .WithMany()
                .HasForeignKey(pm => pm.MaterialId);

            modelBuilder.Entity<ReservaEspacio>()
                .HasKey(re => re.ReservaId);

            modelBuilder.Entity<ReservaEspacio>()
                .HasOne(re => re.Espacio)
                .WithMany(e => e.Reservas)
                .HasForeignKey(re => re.EspacioId);
            
            modelBuilder.Entity<ReservaEspacio>()
                .HasOne(re => re.Alumno)
                .WithMany(a => a.Reservas)
                .HasForeignKey(re => re.AlumnoId);

            modelBuilder.Entity<ReservaEspacio>()
               .HasOne(re => re.Profesor)
               .WithMany(a => a.Reservas)
               .HasForeignKey(re => re.ProfesorId);

            modelBuilder.Entity<RefreshToken>()
                .HasOne(rt => rt.Usuario)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(rt => rt.NumeroCuenta)
                .HasPrincipalKey(u => u.NumeroCuenta);


        }
    }
}
