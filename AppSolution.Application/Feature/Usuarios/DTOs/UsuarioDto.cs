namespace AppSolution.Application.Features.Usuarios.DTOs;

public record UsuarioDto
(
    Guid Id,
    string Nome,
    string Email
);