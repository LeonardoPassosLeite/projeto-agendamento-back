using Agendamento.Domain.Validation;

namespace Agendamento.Domain.Entities
{
    public sealed class Categoria : BaseEntity
    {
        public bool IsActive { get; set; } = true;
        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Categoria(string nome)
        {
            ValidaExcessoes(nome);
        }

        public Categoria(int id, string nome)
        {
            DomainExceptionValidation.When(id < 0, "Id inválido");

            Id = id;
            ValidaExcessoes(nome);
        }

        public void Update(string nome)
        {
            ValidaExcessoes(nome);
        }

        public void Disable()
        {
            DomainExceptionValidation.When(Produtos.Any(p => p.IsActive), "Não é possível desativar a categoria enquanto houver produtos ativos.");
            IsActive = false;
        }

        private void ValidaExcessoes(string nome)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");
            DomainExceptionValidation.When(nome.Length < 3, "O nome deve ter no mínimo 3 caracteres");
            DomainExceptionValidation.When(nome.Length > 100, "O nome deve ter no máximo 100 caracteres");

            Nome = nome;
        }
    }
}