
using AppSolution.Domain.Exceptions;
using AppSolution.Domain.ValueObjects;

namespace AppSolution.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; private set; }
        public string Nome { get; private set; } = null;
        public Email Email { get; private set; } = null;
        private Usuario()
        {
        }

        public Usuario(string nome, Email email)
        {
            Id = Guid.NewGuid();

            AlterarNome(nome);
            AlterarEmail(email);
        }

        public void AlterarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ValidationException("Nome é obrigatório.");

            Nome = nome;
        }

        public void AlterarEmail(Email email)
        {
            Email = email ?? throw new ValidationException(nameof(email));
        }
    }
}
