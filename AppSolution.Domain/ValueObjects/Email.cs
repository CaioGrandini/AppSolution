using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AppSolution.Domain.Exceptions;

namespace AppSolution.Domain.ValueObjects
{
    public class Email
    {
        public string Endereco { get; }

        public Email(string endereco)
        {
            if (string.IsNullOrWhiteSpace(endereco))
                throw new ValidationException("Email é obrigatório.");

            if (!EhValido(endereco))
                throw new ValidationException("Email inválido.");

            Endereco = endereco;
        }

        private static bool EhValido(string email)
        {
            return Regex.IsMatch(
                email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public override string ToString()
        {
            return Endereco;
        }
    }
}
