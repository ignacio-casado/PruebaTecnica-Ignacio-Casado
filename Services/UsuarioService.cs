using PruebaTecnicaIgnacioCasado.Models.DTOs.Request;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response.DefaultResponse;
using PruebaTecnicaIgnacioCasado.Models.Entities;
using PruebaTecnicaIgnacioCasado.Repositories;
using PruebaTecnicaIgnacioCasado.Repositories.Interfaces;
using PruebaTecnicaIgnacioCasado.Services.Interfaces;

namespace PruebaTecnicaIgnacioCasado.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ICursoRepository _cursoRepository;
        public UsuarioService(IUsuarioRepository usuarioRepository, ICursoRepository cursoRepository)
        {  
            _usuarioRepository = usuarioRepository;
            _cursoRepository = cursoRepository;
        }

        public async Task<UsuarioNuevoResponse> RegistrarNuevoUsuario(UsuarioNuevoRequest dto, int ejecutorId)
        {
            var ejecutor = await _usuarioRepository.GetByIdAsync(ejecutorId);

            if (ejecutor == null || ejecutor.Rol.Id != 1)
            {
                throw new UnauthorizedAccessException("Solo el Director puede registrar nuevos usuarios.");
            }

            
            var nuevoUsuario = new Usuario
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                NumeroDocumento = dto.NumeroDocumento,
                RolId = dto.RolId
            };

            
            var resultado = await _usuarioRepository.AddAsync(nuevoUsuario);

            return new UsuarioNuevoResponse
            {

                Nombre = resultado.Nombre,
                Email = resultado.Email,
                NombreRol = resultado.Rol?.Nombre ?? "Asignado"
            };
        }

        public async Task<IEnumerable<GetAllUsuarios>> ObtenerTodos(int idRol)
        {
            if (idRol != 1)
            {
                throw new UnauthorizedAccessException("No tiene permisos para ver el listado.");
            }

            return await _usuarioRepository.GetAllAsync();
        }

        public async Task<DefaultResponse> InscribirAlumnoACurso(int idRol, InscripcionCursoRequest request)
        {

            if (idRol != 1 && idRol != 2)
            {
                return new DefaultResponse
                {
                    Mensaje = "No tiene permisos para realizar esta acción. Solo el Director o un Profesor pueden inscribir alumnos.",
                    Status = 403 
                };
            }
            var alumno = await _usuarioRepository.GetByIdWithCursosAsync(request.AlumnoId);
            var curso = await _cursoRepository.GetByIdAsync(request.CursoId);

            if (alumno == null || curso == null)
            {
                return new DefaultResponse
                {
                    Mensaje = $"No se encuentra el alumno con id: {request.AlumnoId} o el curso con id: {request.CursoId}",
                    Status = 404
                };
            }

            if (alumno.CursosInscritos.Any(c => c.Id == request.CursoId))
            {
                return new DefaultResponse
                {
                    Mensaje = $"El alumno con id: {request.AlumnoId} ya se encuentra inscripto en el curso con id: {request.CursoId}",
                    Status = 208
                };
            }

            alumno.CursosInscritos.Add(curso);


            var exito = await _usuarioRepository.UpdateAsync(alumno);

            if (exito) 
            {
                return new DefaultResponse
                {
                    Mensaje = $"El alumno con id: {request.AlumnoId} ha sido agregado al curso con id: {request.CursoId}",
                    Status = 200
                };
            }

            return new DefaultResponse
            {
                Mensaje = "Error inesperado al actualizar la base de datos.",
                Status = 500
            };
        }
    }
}
