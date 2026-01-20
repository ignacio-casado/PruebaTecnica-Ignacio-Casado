using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaIgnacioCasado.Models.DTOs.Request
{
    public class CursoNuevoRequest
    {
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(8)]
        public string NumeroDocumentoProfesor { get; set; } = string.Empty;
    }
}
