using AppSolution.Application.Feature.Usuarios.Commands;
using AppSolution.Application.Features.Usuarios.Queries.ObterUsuarioPorId;
using AppSolution.Domain.Entities;
using AppSolution.Domain.Interfaces;
using AppSolution.Domain.ValueObjects;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Tests.Feature.Usuarios.Command
{
    public class ExcluirUsuarioCommandHandlerTests
    {
        [Fact]
        public async Task Excluir_Usuario()
        {
            var usuario = new Usuario("Caio", new Email("caio@gmail.com"));

            var repositoryMock = new Mock<IUsuarioRepository>();

            repositoryMock.Setup(x => x.ObterPorIdAsync(usuario.Id, It.IsAny<CancellationToken>())).ReturnsAsync(usuario);

            var handler = new ExcluirUsuarioCommandsHandler(repositoryMock.Object);

            var command = new ExcluirUsuarioCommands(usuario.Id);

            await handler.Handle(command);

            // Assert
            repositoryMock.Verify(x => x.ExcluirUsuario(usuario), Times.Once);
        }
    }
}
