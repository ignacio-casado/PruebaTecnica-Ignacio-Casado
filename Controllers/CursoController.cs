using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Request;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response.DefaultResponse;
using PruebaTecnicaIgnacioCasado.Services.Interfaces;

namespace PruebaTecnicaIgnacioCasado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CursoController : ControllerBase 
    {
        private readonly ICursoService _cursoService;

        public CursoController(ICursoService cursoService)
        {
            _cursoService = cursoService;
        }
        /// <summary>
        /// Genera el alta de un nuevo curso , solo el director lo puede hacer
        /// </summary>
        /// <returns></returns>
        [HttpPost("Alta")]
        public async Task<IActionResult> Crear([FromBody] CursoNuevoRequest request, [FromHeader] int idRol)
        {


            try
            {
                var response = await _cursoService.CrearCurso(idRol, request);
                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new DefaultResponse
                {
                    Mensaje = "Error interno al intentar crear el curso.",
                    Status = 500
                });
            }
        }

        /// <summary>
        /// Endpoint para obtener el listado de alumnos de un curso específico.
        /// </summary>
        [HttpGet("ListarAlumnosPorCurso")]
        public async Task<IActionResult> ListarAlumnosInscriptos([FromHeader] int idRol, [FromHeader] int idCurso)
        {
            try
            {
                var alumnos = await _cursoService.ObtenerAlumnosPorCurso(idRol, idCurso);
                return Ok(alumnos);
            }
            catch (UnauthorizedAccessException ex)
            { 
                return StatusCode(403, new { mensaje = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {      
                return NotFound(new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al obtener el listado de alumnos." });
            }
        }
    }
}
