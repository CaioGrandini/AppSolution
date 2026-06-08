using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSolution.Domain.Entities;
using AppSolution.Domain.Interfaces;
using AppSolution.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppSolution.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(x => x.Email.Endereco == email);
        }

        public async Task<IEnumerable<Usuario>> ObterTodosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task AdicionarAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task<int> ContarAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Usuarios.CountAsync(cancellationToken);
        }

        public async Task<List<Usuario>> ObterPaginadoAsync(int page, int pageSize, CancellationToken cancellationToken = default)
        {
            return await _context.Usuarios
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);

            await _context.SaveChangesAsync();
        }

        public async Task ExcluirUsuario(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);

            await _context.SaveChangesAsync();
        }
    }
}
