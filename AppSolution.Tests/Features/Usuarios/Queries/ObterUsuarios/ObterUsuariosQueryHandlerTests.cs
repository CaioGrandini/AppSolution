using AppSolution.Application.Features.Usuarios.Queries.ObterUsuarios;
using AppSolution.Domain.Entities;
using AppSolution.Domain.Interfaces;
using AppSolution.Domain.ValueObjects;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Tests.Features.Usuarios.Queries.ObterUsuarios
{
    public class ObterUsuariosQueryHandlerTests
    {
        [Fact]
        public async Task Deve_Retornar_Usuarios_Paginados()
        {
            // Arrange

            var usuarios = new List<Usuario>
            {
                new(
                    "Caio",
                    new Email("caio@gmail.com")),

                new(
                    "Maria",
                    new Email("maria@gmail.com"))
            };

            var repositoryMock =
                new Mock<IUsuarioRepository>();

            repositoryMock
                .Setup(x => x.ContarAsync(
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(2);

            repositoryMock
                .Setup(x => x.ObterPaginadoAsync(
                    1,
                    10,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(usuarios);

            var handler = new ObterUsuariosQueryHandler(repositoryMock.Object);

            var query = new ObterUsuariosQuery(1, 10);

            // Act
            var resultado = await handler.Handle(query);

            // Assert
            resultado.Should().NotBeNull();

            resultado.TotalItems.Should().Be(2);

            resultado.Page.Should().Be(1);

            resultado.PageSize.Should().Be(10);

            resultado.Items.Should().HaveCount(2);

            resultado.Items[0].Nome.Should().Be("Caio");

            resultado.Items[1].Nome.Should().Be("Maria");
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Existirem_Usuarios()
        {
            // Arrange

            var repositoryMock = new Mock<IUsuarioRepository>();

            repositoryMock
                .Setup(x => x.ContarAsync(
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(0);

            repositoryMock
                .Setup(x => x.ObterPaginadoAsync(
                    1,
                    10,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync([]);

            var handler = new ObterUsuariosQueryHandler(repositoryMock.Object);

            var query =
                new ObterUsuariosQuery(1, 10);

            // Act
            var resultado = await handler.Handle(query);

            // Assert
            resultado.TotalItems.Should().Be(0);
            resultado.Items.Should().BeEmpty();
        }
    }
}
