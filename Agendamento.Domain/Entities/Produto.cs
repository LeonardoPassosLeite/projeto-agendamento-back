using Agendamento.Domain.Exceptions;

namespace Agendamento.Domain.Entities
{
    public sealed class Produto : BaseEntity
    {
        public decimal Preco { get; private set; }
        public string? Descricao { get; private set; }
        public bool IsActive { get; set; } = true;

        public int CategoriaId { get; set; }
        public required Categoria Categoria { get; set; }

        public Produto(string nome, decimal preco, string? descricao, int categoriaId)
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            CategoriaId = categoriaId;

            ValidaExcessoes();
        }

        public Produto(int id, string nome, decimal preco, string? descricao, int categoriaId)
        {
            DomainValidationException.When(id < 0, "Valor de Id é inválido");

            Id = id;
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            CategoriaId = categoriaId;

            ValidaExcessoes();
        }

        public void Update(string nome, decimal preco, string? descricao, int categoriaId)
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            CategoriaId = categoriaId;

            ValidaExcessoes();
        }

        public void Disable()
        {
            IsActive = false;
        }

        private void ValidaExcessoes()
        {
            DomainValidationException.When(string.IsNullOrEmpty(Nome), "Nome é obrigatório");
            DomainValidationException.When(Nome.Length < 3, "Nome deve ter no mínimo 3 caracteres");
            DomainValidationException.When(Nome.Length > 100, "Nome deve ter no máximo 100 caracteres");

            DomainValidationException.When(Preco < 0, "Valor de preço Inválido");

            if (!string.IsNullOrEmpty(Descricao))
            {
                DomainValidationException.When(Descricao.Length < 3, "Descrição deve ter no mínimo 3 caracteres");
                DomainValidationException.When(Descricao.Length > 100, "Descrição deve ter no máximo 100 caracteres");
            }

            DomainValidationException.When(CategoriaId <= 0, "CategoriaId é obrigatório e deve ser maior que zero");
        }
    }
}