
using AppSolution.Domain.Exceptions;
using AppSolution.Domain.Interfaces;
using AppSolution.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Application.Feature.Usuarios.Commands
{
    public class AtualizarUsuarioCommandsHandler
    {
        private readonly IUsuarioRepository _repository;

        public AtualizarUsuarioCommandsHandler(
            IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(
            AtualizarUsuarioCommands command)
        {
            var usuario =
                await _repository.ObterPorIdAsync(command.Id);

            if (usuario is null)
                throw new NotFoundException("Usuário não encontrado.");

            usuario.AlterarNome(command.Nome);

            usuario.AlterarEmail(
                new Email(command.Email));

            await _repository.AtualizarAsync(usuario);
        }
    }
}
