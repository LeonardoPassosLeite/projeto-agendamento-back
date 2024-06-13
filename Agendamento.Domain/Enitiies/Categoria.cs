using Agendamento.Domain.Validation;

namespace Agendamento.Domain.Enitiies
{
    public sealed class Categoria : BaseEntity
    {
        public ICollection<Produto> Produtos { get; set; }

        public Categoria(string nome)
        {
            ValidaExcessoes(nome);
        }

        public Categoria(int id, string nome)
        {
            DomainExceptionValidation.When(id < 0, "Valor de Id é inválido");

            Id = id;
            ValidaExcessoes(nome);
        }

        public void Update(string nome)
        {
            ValidaExcessoes(nome);
        }

        private void ValidaExcessoes(string nome)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");
            DomainExceptionValidation.When(nome.Length < 3, "O nome deve ter no mínimo 3 caracteres");
            DomainExceptionValidation.When(nome.Length > 100, "O nome deve ter no máximo 3 caracteres");

            Nome = nome;
        }
    }
}