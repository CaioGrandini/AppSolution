namespace AppSolution.Application.Features.Usuarios.Queries.ObterUsuarios;

public record ObterUsuariosQuery
(
    int Page = 1,
    int PageSize = 10
);