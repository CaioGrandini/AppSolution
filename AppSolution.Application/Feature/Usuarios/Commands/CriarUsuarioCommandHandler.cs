using AppSolution.Application.Feature.Usuarios.Commands;
using AppSolution.Domain.Entities;
using AppSolution.Domain.Exceptions;
using AppSolution.Domain.Interfaces;
using AppSolution.Domain.ValueObjects;

namespace AppSolution.Application.Features.Usuarios.Commands.CriarUsuario;

public class CriarUsuarioCommandHandler
{
    private readonly IUsuarioRepository _usuarioRepository;

    public CriarUsuarioCommandHandler(
        IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<Guid> Handle(CriarUsuarioCommand command)
    {
        var validator = new CriarUsuarioCommandValidator();

        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors.First().ErrorMessage);
        }

        var email = new Email(command.Email);

        var usuario = new Usuario(command.Nome, email);

        await _usuarioRepository.AdicionarAsync(usuario);

        return usuario.Id;
    }
}