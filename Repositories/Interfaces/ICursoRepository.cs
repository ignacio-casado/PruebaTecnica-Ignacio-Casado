using PruebaTecnicaIgnacioCasado.Models.Entities;

namespace PruebaTecnicaIgnacioCasado.Repositories.Interfaces
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Curso>> GetAllAsync();
        Task<Curso?> GetByIdAsync(int id);
        Task AddAsync(Curso curso);
        Task UpdateAsync(Curso curso);
        Task DeleteAsync(int id);
        Task InscribirAlumnoAsync(int cursoId, int alumnoId);
        Task<Curso> GetByIdWithAlumnosAsync(int id);

    }
}
