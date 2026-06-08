
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
    public class ExcluirUsuarioCommandsHandler
    {
        private readonly IUsuarioRepository _repository;

        public ExcluirUsuarioCommandsHandler(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(ExcluirUsuarioCommands command)
        {
            var usuario = await _repository.ObterPorIdAsync(command.Id);

            if (usuario is null)
                throw new NotFoundException("Usuário não encontrado.");            

            await _repository.ExcluirUsuario(usuario);
        }
    }
}
