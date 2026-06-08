using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolution.Application.Feature.Usuarios.Commands
{
    public record AtualizarEmailUsuarioCommands
    (
        Guid Id,
        string Email
    );
}
