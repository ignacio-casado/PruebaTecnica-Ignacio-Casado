using PruebaTecnicaIgnacioCasado.Models.DTOs.Response.DefaultResponse;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Request;

namespace PruebaTecnicaIgnacioCasado.Services.Interfaces
{
    public interface ICursoService
    {
        Task<DefaultResponse> CrearCurso(int idRol , CursoNuevoRequest dto);
        Task<IEnumerable<UsuarioNuevoResponse>> ObtenerAlumnosPorCurso(int idRol, int cursoId);
    }
}
