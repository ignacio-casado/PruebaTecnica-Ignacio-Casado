using PruebaTecnicaIgnacioCasado.Models.Entities;

namespace PruebaTecnicaIgnacioCasado.Repositories.Interfaces
{
    public interface IRolRepository
    {
        Task<IEnumerable<Rol>> GetAllAsync();
        Task<Rol?> GetByIdAsync(int id);
        Task AddAsync(Rol rol);
        Task<Rol?> GetByNombreAsync(string nombre);
    }
}
