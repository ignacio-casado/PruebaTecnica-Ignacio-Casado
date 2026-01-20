using PruebaTecnicaIgnacioCasado.Models.DTOs.Request;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response.DefaultResponse;
using PruebaTecnicaIgnacioCasado.Models.Entities;
using PruebaTecnicaIgnacioCasado.Repositories;
using PruebaTecnicaIgnacioCasado.Repositories.Interfaces;
using PruebaTecnicaIgnacioCasado.Services.Interfaces;

namespace PruebaTecnicaIgnacioCasado.Services
{
    public class CursoService : ICursoService
    {

        private readonly ICursoRepository _cursoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public CursoService (ICursoRepository cursoRepository, IUsuarioRepository usuarioRepository)
        {
            _cursoRepository = cursoRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<DefaultResponse> CrearCurso(int idRol, CursoNuevoRequest dto)
        {

            if (idRol != 1)
            {
                return new DefaultResponse { Mensaje = "No tiene permisos para crear cursos.", Status = 403 };
            }

            var profesor = await _usuarioRepository.GetByDocumentoAsync(dto.NumeroDocumentoProfesor);

            
            if (profesor == null || profesor.RolId != 2)
            {
                return new DefaultResponse
                {
                    Mensaje = $"No se encontró un profesor con el documento {dto.NumeroDocumentoProfesor}.",
                    Status = 404
                };
            }

         
            var nuevoCurso = new Curso
            {
                Nombre = dto.Nombre,
                ProfesorId = profesor.Id 
            };

            await _cursoRepository.AddAsync(nuevoCurso);

            return new DefaultResponse
            {
                Mensaje = $"Curso '{dto.Nombre}' creado exitosamente. Profesor asignado: {profesor.Nombre}.",
                Status = 201
            };
        }

        public async Task<IEnumerable<UsuarioNuevoResponse>> ObtenerAlumnosPorCurso(int idRol, int cursoId)
        {
           
            if (idRol != 1 && idRol != 2)
            {
                throw new UnauthorizedAccessException("No tiene permisos para ver el listado.");
            }


            var curso = await _cursoRepository.GetByIdWithAlumnosAsync(cursoId);

            if (curso == null)
            {
           
                throw new KeyNotFoundException($"El curso con ID {cursoId} no existe en la base de datos.");
            }

     
            return curso.Alumnos
                .Where(a => a.RolId == 3)
                .Select(alumno => new UsuarioNuevoResponse
                {
                    Nombre = $"{alumno.Nombre} {alumno.Apellido}",
                    Email = alumno.Email,
                    NombreRol = "Alumno"
                }).ToList();
        }
    }
}
