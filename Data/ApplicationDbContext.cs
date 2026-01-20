using Microsoft.EntityFrameworkCore;
using PruebaTecnicaIgnacioCasado.Models.Entities;

namespace PruebaTecnicaIgnacioCasado.Data
{
    public class ApplicationDbContext : DbContext
    {
   
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.NumeroDocumento)
                .IsUnique();

            modelBuilder.Entity<Usuario>()
            .HasMany(u => u.CursosInscritos)
            .WithMany(c => c.Alumnos)
            .UsingEntity<Dictionary<string, object>>(
                "Inscripciones",
                j => j.HasOne<Curso>().WithMany().HasForeignKey("CursoId"),
                j => j.HasOne<Usuario>().WithMany().HasForeignKey("UsuarioId")
            );

            modelBuilder.Entity<Curso>()
                .HasOne(c => c.Profesor)
                .WithMany(u => u.CursosDictados)
                .HasForeignKey(c => c.ProfesorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Nombre = "Director" },
                new Rol { Id = 2, Nombre = "Profesor" },
                new Rol { Id = 3, Nombre = "Alumno" }
            );

           
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = 1,
                    Nombre = "Claudio",
                    Apellido= "General",
                    Email = "admin@colegio.com",
                    NumeroDocumento = "12311678",
                    RolId = 1 // Asignado como Director
                } ,
                new Usuario
                {
                    Id = 2,
                    Nombre = "Juan",
                    Apellido = "Perez",
                    Email = "profe.juan@colegio.com",
                    NumeroDocumento = "12345699",
                    RolId = 2 // Profesor
                },
                new Usuario
                {
                    Id = 3,
                    Nombre = "Juan Perez",
                    Apellido = " Perez",
                    Email = "alumno.juan@colegio.com",
                    NumeroDocumento = "66345699",
                    RolId = 3 // Profesor
                }
            );




    }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Curso> Cursos { get; set; }
    }
}
