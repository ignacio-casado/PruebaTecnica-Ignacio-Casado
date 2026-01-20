using PruebaTecnicaIgnacioCasado.Models.DTOs.Request;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response.DefaultResponse;
using PruebaTecnicaIgnacioCasado.Models.Entities;

namespace PruebaTecnicaIgnacioCasado.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioNuevoResponse> RegistrarNuevoUsuario(UsuarioNuevoRequest dto, int ejecutorId);
        Task<IEnumerable<GetAllUsuarios>> ObtenerTodos(int idRol);

        Task<DefaultResponse> InscribirAlumnoACurso(int idRol, InscripcionCursoRequest request);
    }
}
