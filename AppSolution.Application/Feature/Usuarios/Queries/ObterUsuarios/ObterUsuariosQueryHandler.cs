using AppSolution.Application.Common.Pagination;
using AppSolution.Application.Features.Usuarios.DTOs;
using AppSolution.Domain.Interfaces;

namespace AppSolution.Application.Features.Usuarios.Queries.ObterUsuarios;

public class ObterUsuariosQueryHandler
{
    private readonly IUsuarioRepository _repository;

    public ObterUsuariosQueryHandler(
        IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<UsuarioDto>> Handle(ObterUsuariosQuery query)
    {
        var totalItems = await _repository.ContarAsync();

        var usuarios =
            await _repository.ObterPaginadoAsync(
                query.Page,
                query.PageSize);

        return new PagedResult<UsuarioDto>
        {
            Items = usuarios.Select(x =>
                new UsuarioDto(
                        x.Id,
                        x.Nome,
                        x.Email.Endereco))
                    .ToList(),

            TotalItems = totalItems,

            Page = query.Page,

            PageSize = query.PageSize
        };
    }
}