using AppSolution.Domain.Entities;
using AppSolution.Domain.Interfaces;

namespace AppSolution.UnitTests.Fakes;

public class FakeUsuarioRepository : IUsuarioRepository
{
    private readonly List<Usuario> _usuarios = [];

    public Task AdicionarAsync(Usuario usuario)
    {
        _usuarios.Add(usuario);

        return Task.CompletedTask;
    }

    public Task<Usuario?> ObterPorEmailAsync(string email)
    {
        return Task.FromResult(_usuarios.FirstOrDefault(x => x.Email.Endereco == email));
    }

    public Task<Usuario?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_usuarios.FirstOrDefault(x => x.Id == id));
    }

    public Task<IEnumerable<Usuario>> ObterTodosAsync()
    {
        return Task.FromResult(_usuarios.AsEnumerable());
    }

    public Task<int> ContarAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_usuarios.Count);
    }

    public Task<List<Usuario>> ObterPaginadoAsync(int page, int pageSize, CancellationToken cancellationToken = default)
    {
        var resultado = _usuarios
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return Task.FromResult(resultado);
    }

    public Task AtualizarAsync(Usuario usuario)
    {
        return Task.CompletedTask;
    }

    public Task ExcluirUsuario(Usuario usuario)
    {
        _usuarios.Remove(usuario);
        return Task.CompletedTask;
    }
}