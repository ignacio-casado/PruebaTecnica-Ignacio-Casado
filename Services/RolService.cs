using PruebaTecnicaIgnacioCasado.Models.DTOs.Request;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response.DefaultResponse;
using PruebaTecnicaIgnacioCasado.Models.Entities;
using PruebaTecnicaIgnacioCasado.Repositories.Interfaces;
using PruebaTecnicaIgnacioCasado.Services.Interfaces;

namespace PruebaTecnicaIgnacioCasado.Services
{
    public class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;

        public RolService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<IEnumerable<Rol>> Listado()
        {
            return await _rolRepository.GetAllAsync();
        }

        public async Task<DefaultResponse> AgregarRol(int idRol, NuevoRolRequest request)
        {
           
            if (idRol != 1)
            {
                return new DefaultResponse
                {
                    Mensaje = "Acceso denegado. Solo el Director puede dar de alta nuevos roles en el sistema.",
                    Status = 403
                };
            }

            var existe = await _rolRepository.GetByNombreAsync(request.Nombre.ToLower());
            if (existe != null)
            {
                return new DefaultResponse
                {
                    Mensaje = $"El rol '{request.Nombre}' ya existe.",
                    Status = 400
                };
            }

            var nuevoRol = new Rol
            {
                Nombre = request.Nombre
            };

            await _rolRepository.AddAsync(nuevoRol);

            return new DefaultResponse
            {
                Mensaje = $"Rol '{request.Nombre}' creado exitosamente.",
                Status = 201
            };
        }
    }
    
}
