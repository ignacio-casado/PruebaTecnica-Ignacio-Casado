using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnicaIgnacioCasado.Models.Entities
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } // Esta es la UNICA clave primaria

        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [StringLength(8)]
        public string NumeroDocumento { get; set; }

        public int RolId { get; set; }
        public Rol Rol { get; set; } = null!;

        public ICollection<Curso> CursosDictados { get; set; } = new List<Curso>();
        public ICollection<Curso> CursosInscritos { get; set; } = new List<Curso>();
    }
}
