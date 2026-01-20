namespace PruebaTecnicaIgnacioCasado.Models.DTOs.Response
{
    public class GetAllUsuarios
    {
        public string NombreCompleto { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string NumeroDocumento { get; set; }

        public string NombreRol { get; set; }
    }
}
