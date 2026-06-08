using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSolution.Domain.Entities;

namespace AppSolution.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<Usuario?> ObterPorEmailAsync(string email);
        Task<IEnumerable<Usuario>> ObterTodosAsync();
        Task AdicionarAsync(Usuario usuario);
        Task<int> ContarAsync(CancellationToken cancellationToken = default);
        Task<List<Usuario>> ObterPaginadoAsync(int page, int pageSize, CancellationToken cancellationToken = default);
        Task AtualizarAsync(Usuario usuario);
        Task ExcluirUsuario(Usuario usuario);
    }
}
