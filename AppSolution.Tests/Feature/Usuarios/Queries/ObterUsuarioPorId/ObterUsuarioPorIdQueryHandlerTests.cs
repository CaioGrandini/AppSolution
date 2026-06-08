using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppSolution.Application.Features.Usuarios.Queries.ObterUsuarioPorId;
using AppSolution.Domain.Entities;
using AppSolution.Domain.Interfaces;
using AppSolution.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace AppSolution.Tests.Feature.Usuarios.Queries.ObterUsuarioPorId
{
    public class ObterUsuarioPorIdQueryHandlerTests
    {
        [Fact]
        public async Task Deve_Retornar_Usuario_Quando_Existir()
        {
            // Arrange

            var usuario =
                new Usuario(
                    "Caio",
                    new Email("caio@gmail.com"));

            var repositoryMock =
                new Mock<IUsuarioRepository>();

            repositoryMock
                .Setup(x => x.ObterPorIdAsync(
                    usuario.Id,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(usuario);

            var handler =
                new ObterUsuarioPorIdQueryHandler(
                    repositoryMock.Object);

            var query =
                new ObterUsuarioPorIdQuery(
                    usuario.Id);

            // Act

            var resultado = await handler.Handle(query);

            // Assert

            resultado.Should().NotBeNull();

            resultado.Id.Should().Be(usuario.Id);

            resultado.Nome.Should().Be("Caio");

            resultado.Email.Should().Be("caio@gmail.com");
        }

        [Fact]
        public async Task Deve_Lancar_NotFoundException_Quando_Usuario_Nao_Existir()
        {
            // Arrange

            var repositoryMock =
                new Mock<IUsuarioRepository>();

            repositoryMock
                .Setup(x => x.ObterPorIdAsync(
                    It.IsAny<Guid>(),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Usuario?)null);

            var handler =
                new ObterUsuarioPorIdQueryHandler(
                    repositoryMock.Object);

            var query =
                new ObterUsuarioPorIdQuery(
                    Guid.NewGuid());

            // Act

            Func<Task> act =
                async () => await handler.Handle(query);

            // Assert

            await act.Should()
                .ThrowAsync<
                    AppSolution.Domain.Exceptions.NotFoundException>()
                .WithMessage(
                    "Usuário não encontrado.");
        }
    }
}
