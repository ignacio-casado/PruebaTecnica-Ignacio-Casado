using Microsoft.EntityFrameworkCore;
using PruebaTecnicaIgnacioCasado.Data;
using PruebaTecnicaIgnacioCasado.Models.Entities;
using PruebaTecnicaIgnacioCasado.Repositories.Interfaces;

namespace PruebaTecnicaIgnacioCasado.Repositories
{
    public class CursoRepository : ICursoRepository
    {
        private readonly ApplicationDbContext _context;
        public CursoRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Curso>> GetAllAsync()
        {
            return await _context.Cursos
                .Include(c => c.Profesor)
                .Include(c => c.Alumnos)
                .ToListAsync();
        }

        public async Task<Curso?> GetByIdAsync(int id)
        {
            return await _context.Cursos
                .Include(c => c.Profesor)
                .Include(c => c.Alumnos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Curso curso)
        {
            await _context.Cursos.AddAsync(curso);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Curso curso)
        {
            _context.Cursos.Update(curso);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var curso = await _context.Cursos.FindAsync(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
                await _context.SaveChangesAsync();
            }
        }

        public async Task InscribirAlumnoAsync(int cursoId, int alumnoId)
        {
            var curso = await _context.Cursos.Include(c => c.Alumnos).FirstOrDefaultAsync(c => c.Id == cursoId);
            var alumno = await _context.Usuarios.FindAsync(alumnoId);

            if (curso != null && alumno != null)
            {
                curso.Alumnos.Add(alumno);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Curso?> GetByIdWithAlumnosAsync(int id)
        {
            return await _context.Cursos
                .Include(u => u.Alumnos) // IMPORTANTE: Carga la lista de cursos       // Opcional: Si necesitas validar su rol después
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
