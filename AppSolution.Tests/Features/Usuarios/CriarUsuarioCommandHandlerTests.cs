
using AppSolution.Application.Feature.Usuarios.Commands;
using AppSolution.Application.Features.Usuarios.Commands.CriarUsuario;
using AppSolution.Domain.Exceptions;
using AppSolution.UnitTests.Fakes;
using FluentAssertions;

namespace AppSolution.UnitTests.Features.Usuarios.CriarUsuario;

public class CriarUsuarioCommandHandlerTests
{
    [Fact]
    public async Task Deve_Criar_Usuario_Quando_Command_For_Valido()
    {
        // Arrange
        var repository = new FakeUsuarioRepository();

        var handler = new CriarUsuarioCommandHandler(repository);

        var command = new CriarUsuarioCommand(
            "Caio",
            "caio@email.com");

        // Act
        var usuarioId = await handler.Handle(command);

        // Assert
        usuarioId.Should().NotBeEmpty();

        var usuario = await repository.ObterPorIdAsync(usuarioId);

        usuario.Should().NotBeNull();

        usuario!.Nome.Should().Be("Caio");

        usuario.Email.Endereco.Should().Be("caio@email.com");
    }

    [Fact]
    public async Task Nao_Deve_Criar_Usuario_Com_Email_Invalido()
    {
        // Arrange
        var repository = new FakeUsuarioRepository();

        var handler = new CriarUsuarioCommandHandler(repository);

        var command = new CriarUsuarioCommand(
            "Caio",
            "email-invalido");

        // Act
        Func<Task> act = async () => await handler.Handle(command);

        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }
}