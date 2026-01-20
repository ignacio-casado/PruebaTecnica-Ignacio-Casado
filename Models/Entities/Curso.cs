using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaTecnicaIgnacioCasado.Models.Entities
{
    public class Curso
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        public string Nombre { get; set; } = string.Empty;

        public int ProfesorId { get; set; }

        public Usuario Profesor { get; set; } = null!;

        public ICollection<Usuario> Alumnos { get; set; } = new List<Usuario>();
    }
}
