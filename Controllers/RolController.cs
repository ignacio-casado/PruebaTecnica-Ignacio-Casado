using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Request;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response;
using PruebaTecnicaIgnacioCasado.Services.Interfaces;

namespace PruebaTecnicaIgnacioCasado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        /// <summary>
        /// Obtiene el listado de todos los roles disponibles.
        /// </summary>
        /// <remarks>
        /// Este endpoint es útil para llenar combos o selects en el frontend 
        /// al momento de crear un nuevo usuario.
        /// </remarks>
        [HttpGet("Listado")]
        public async Task<IActionResult> Listado()
        {
            try
            {
                var roles = await _rolService.Listado();
                return Ok(roles);
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno al obtener los roles.");
            }
        }

        /// <summary>
        /// Permite dar de alta un nuevo Rol en el sistema.
        /// </summary>
        [HttpPost("Agregar")]
        public async Task<IActionResult> AgregarRol([FromHeader] int idRol, [FromBody] NuevoRolRequest request)
        {
            if (string.IsNullOrEmpty(request.Nombre))
            {
                return BadRequest(new { mensaje = "El nombre del rol es obligatorio." });
            }

            try
            {
                var response = await _rolService.AgregarRol(idRol, request);
                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error inesperado al procesar el rol.", detalle = ex.Message });
            }
        }
    }
}