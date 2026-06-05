using AppSolution.Application.Feature.Usuarios.DTOs;
using AppSolution.Domain.Exceptions;
using AppSolution.Domain.Interfaces;

namespace AppSolution.Application.Features.Usuarios.Queries.ObterUsuarioPorId;

public class ObterUsuarioPorIdQueryHandler
{
    private readonly IUsuarioRepository _repository;

    public ObterUsuarioPorIdQueryHandler(
        IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<UsuarioDetalheDto?> Handle(
        ObterUsuarioPorIdQuery query)
    {
        var usuario = await _repository.ObterPorIdAsync(query.Id);

        if (usuario is null)
        {
            throw new NotFoundException("Usuário não encontrado.");
        }

        return new UsuarioDetalheDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email.Endereco
        };
    }
}