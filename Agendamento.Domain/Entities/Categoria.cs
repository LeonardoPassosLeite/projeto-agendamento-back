using Agendamento.Domain.Exceptions;

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
            ValidationException.When(id < 0, "Id inválido");

            Id = id;
            ValidaExcessoes(nome);
        }

        public void Update(string nome)
        {
            ValidaExcessoes(nome);
        }

        public void Disable()
        {
            ValidationException.When(Produtos.Any(p => p.IsActive), "Não é possível desativar a categoria enquanto houver produtos ativos.");
            IsActive = false;
        }

        private void ValidaExcessoes(string nome)
        {
            ValidationException.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");
            ValidationException.When(nome.Length < 3, "O nome deve ter no mínimo 3 caracteres");
            ValidationException.When(nome.Length > 100, "O nome deve ter no máximo 100 caracteres");

            Nome = nome;
        }
    }
}