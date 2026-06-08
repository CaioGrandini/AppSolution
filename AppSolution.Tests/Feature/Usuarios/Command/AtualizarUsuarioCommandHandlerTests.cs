using AppSolution.Application.Feature.Usuarios.Commands;
using AppSolution.Domain.Entities;
using AppSolution.Domain.Exceptions;
using AppSolution.Domain.ValueObjects;
using AppSolution.UnitTests.Fakes;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Tests.Feature.Usuarios.Command
{
    public class AtualizarUsuarioCommandHandlerTests
    {
        [Fact]
        public async Task Deve_Atualizar_Nome_E_Email()
        {
            // Arrange

            var repository = new FakeUsuarioRepository();

            var usuario =
                new AppSolution.Domain.Entities.Usuario(
                    "Caio",
                    new Email("caio@email.com"));

            await repository.AdicionarAsync(usuario);

            var handler =
                new AtualizarUsuarioCommandsHandler(repository);

            var command =
                new AtualizarUsuarioCommands(
                    usuario.Id,
                    "Caio Atualizado",
                    "novo@email.com");

            // Act
            await handler.Handle(command);

            // Assert
            var usuarioAtualizado = await repository.ObterPorIdAsync(usuario.Id);

            usuarioAtualizado.Should().NotBeNull();

            usuarioAtualizado!.Nome
                .Should()
                .Be("Caio Atualizado");

            usuarioAtualizado.Email.Endereco
                .Should()
                .Be("novo@email.com");
        }

        [Fact]
        public async Task Deve_Lancar_NotFound_Quando_Usuario_Nao_Existir()
        {
            // Arrange
            var repository = new FakeUsuarioRepository();

            var handler =
                new AtualizarUsuarioCommandsHandler(repository);

            var command = new AtualizarUsuarioCommands(
                    Guid.NewGuid(),
                    "Caio",
                    "caio@email.com");

            // Act
            Func<Task> act = async () => await handler.Handle(command);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task Nao_Deve_Atualizar_Email_Invalido()
        {
            // Arrange
            var repository = new FakeUsuarioRepository();

            var usuario =
                new Usuario(
                    "Caio",
                    new Email("caio@email.com"));

            await repository.AdicionarAsync(usuario);

            var handler = new AtualizarUsuarioCommandsHandler(repository);

            var command = new AtualizarUsuarioCommands(
                    usuario.Id,
                    "Caio Atualizado",
                    "email-invalido");

            // Act
            Func<Task> act = async () => await handler.Handle(command);

            // Assert
            await act.Should().ThrowAsync<ValidationException>();
        }
    }
}
