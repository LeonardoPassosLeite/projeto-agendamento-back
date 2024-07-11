using Agendamento.Domain.Exceptions;

namespace Agendamento.Domain.Entities
{
    public sealed class Categoria : SimpleEntity
    {
        public bool IsActive { get; set; } = true;
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Categoria(string nome)
        {
            IniciaValor(nome);
        }

        public Categoria(int id, string nome)
        {
            DomainValidationException.When(id < 0, "Id inválido");

            Id = id;
            IniciaValor(nome);
        }

        public void Update(string nome)
        {
            IniciaValor(nome);
        }

        public void Disable()
        {
            DomainValidationException.When(Produtos.Any(p => p.IsActive), "Não é possível desativar a categoria enquanto houver produtos ativos.");
            IsActive = false;
        }

        private void IniciaValor(string nome)
        {
            Nome = nome;

            ValidaExcessoes();
        }

        private void ValidaExcessoes()
        {
            DomainValidationException.When(string.IsNullOrEmpty(Nome), "Nome é obrigatório");
            DomainValidationException.When(Nome.Length < 3, "O nome deve ter no mínimo 3 caracteres");
            DomainValidationException.When(Nome.Length > 100, "O nome deve ter no máximo 100 caracteres");
        }
    }
}