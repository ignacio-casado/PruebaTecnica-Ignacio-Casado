using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Request;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response.DefaultResponse;
using PruebaTecnicaIgnacioCasado.Services;

namespace PruebaTecnicaIgnacioCasado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }


        /// <summary>
        /// Alta de nuevo usuario solo el director (rol 1) lo puede generar
        /// </summary>
        /// <returns></returns>
        [HttpPost("Alta")]
        public async Task<ActionResult<UsuarioNuevoResponse>> Alta([FromBody] UsuarioNuevoRequest request, [FromHeader] int ejecutorId)
        {
            try
            {

                var resultado = await _usuarioService.RegistrarNuevoUsuario(request, ejecutorId);

                return Ok(resultado);
            }
            catch (UnauthorizedAccessException ex)
            {
               
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
         
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        /// <summary>
        /// Lista todos los usuarios de la institucion , solo el director puede ejecutarlo 
        /// </summary>
        /// <returns></returns>
        [HttpGet("ListadoCompleto")]
        public async Task<ActionResult<IEnumerable<GetAllUsuarios>>> ListarTodos([FromHeader] int Rol)
        {
            try
            {
                var usuarios = await _usuarioService.ObtenerTodos(Rol);
                return Ok(usuarios);
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { mensaje = ex.Message });
            }
            catch (Exception ex)
            {
               
                return StatusCode(500, new { mensaje = "Ocurrió un error interno al obtener el listado." });
            }
        }

        /// <summary>
        /// Inscribe alumno a nuevo curso , puede ejecutarlo el profesor y el director (rol 1 y 2)
        /// </summary>
        /// <returns></returns>
        [HttpPost("InscribirACurso")]
        public async Task<IActionResult> Inscribir([FromBody] InscripcionCursoRequest request, [FromHeader] int ejecutorRolId)
        {
            try
            {
                var response = await _usuarioService.InscribirAlumnoACurso(ejecutorRolId, request);

                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {        
                return StatusCode(500, new DefaultResponse
                {
                    Mensaje = "Error crítico al procesar la inscripción.",
                    Status = 500
                });
            }
        }
    }
}