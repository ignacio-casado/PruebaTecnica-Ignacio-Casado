using PruebaTecnicaIgnacioCasado.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaIgnacioCasado.Models.DTOs.Request
{
    public class UsuarioNuevoRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [StringLength(8)]
        public string NumeroDocumento { get; set; }
        public int RolId { get; set; }
        
    }
}
