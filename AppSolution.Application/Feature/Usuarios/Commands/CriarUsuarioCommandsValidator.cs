using FluentValidation;

namespace AppSolution.Application.Feature.Usuarios.Commands.CriarUsuario;

public class CriarUsuarioCommandsValidator : AbstractValidator<CriarUsuarioCommands>
{
    public CriarUsuarioCommandsValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("Nome é obrigatório.");

        RuleFor(x => x.Nome)
            .MaximumLength(200)
            .WithMessage("Nome deve possuir no máximo 200 caracteres.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email é obrigatório.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email inválido.");
    }
}