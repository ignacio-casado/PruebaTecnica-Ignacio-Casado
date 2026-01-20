using PruebaTecnicaIgnacioCasado.Models.DTOs.Request;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response.DefaultResponse;
using PruebaTecnicaIgnacioCasado.Models.Entities;

namespace PruebaTecnicaIgnacioCasado.Services.Interfaces
{
    public interface IRolService
    {
        Task<IEnumerable<Rol>> Listado();

        Task<DefaultResponse> AgregarRol(int idRol, NuevoRolRequest request);
    }
}
