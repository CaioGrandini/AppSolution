using AppSolution.Application.Feature.Usuarios.Commands;
using FluentValidation;

namespace AppSolution.Application.Features.Usuarios.Commands.CriarUsuario;

public class CriarUsuarioCommandValidator : AbstractValidator<CriarUsuarioCommand>
{
    public CriarUsuarioCommandValidator()
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