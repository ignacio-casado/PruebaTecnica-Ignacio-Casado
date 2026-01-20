using PruebaTecnicaIgnacioCasado.Models.DTOs.Response;
using PruebaTecnicaIgnacioCasado.Models.Entities;

namespace PruebaTecnicaIgnacioCasado.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<GetAllUsuarios>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task<Usuario?> GetByEmailAsync(string email); 
        Task<Usuario> AddAsync(Usuario usuario);
        Task <bool>  UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task<Usuario> GetByIdWithCursosAsync(int id);

        Task<Usuario?> GetByDocumentoAsync(string documento);
    }
}
