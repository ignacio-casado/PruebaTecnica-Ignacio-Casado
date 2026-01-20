using Microsoft.EntityFrameworkCore;
using PruebaTecnicaIgnacioCasado.Data;
using PruebaTecnicaIgnacioCasado.Models.Entities;
using PruebaTecnicaIgnacioCasado.Repositories.Interfaces;

namespace PruebaTecnicaIgnacioCasado.Repositories
{

    public class RolRepository : IRolRepository
    {
        private readonly ApplicationDbContext _context;
        public RolRepository(ApplicationDbContext context) => _context = context;

        public async Task<IEnumerable<Rol>> GetAllAsync() => await _context.Roles.ToListAsync();
        public async Task<Rol?> GetByIdAsync(int id) => await _context.Roles.FindAsync(id);

        public async Task<Rol?> GetByNombreAsync(string nombre)
        {
          
            return await _context.Roles
                .FirstOrDefaultAsync(r => r.Nombre.ToLower() == nombre.ToLower());
        }

        public async Task AddAsync(Rol rol)
        {
            await _context.Roles.AddAsync(rol);
            await _context.SaveChangesAsync();
        }
    }
}
