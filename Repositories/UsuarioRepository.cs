using Microsoft.EntityFrameworkCore;
using PruebaTecnicaIgnacioCasado.Data;
using PruebaTecnicaIgnacioCasado.Models.DTOs.Response;
using PruebaTecnicaIgnacioCasado.Models.Entities;
using PruebaTecnicaIgnacioCasado.Repositories.Interfaces;

namespace PruebaTecnicaIgnacioCasado.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetAllUsuarios>> GetAllAsync()
        {
            return await _context.Usuarios
                .Select(u => new GetAllUsuarios
                {
                    NombreCompleto = u.Nombre + " " + u.Apellido,
                    Email = u.Email,
                    NumeroDocumento = u.NumeroDocumento,
                    NombreRol = u.Rol.Nombre
                })
                .ToListAsync();
        }

        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios.Include(u => u.Rol)
                                         .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario> AddAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> UpdateAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task DeleteAsync(int id)
        {
            var usuario = await GetByIdAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Usuario> GetByIdWithCursosAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.CursosInscritos)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<Usuario?> GetByDocumentoAsync(string documento)
        {
            throw new NotImplementedException();
        }
    }
}
