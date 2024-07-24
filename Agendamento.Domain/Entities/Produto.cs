using Agendamento.Domain.Exceptions;

namespace Agendamento.Domain.Entities
{
    public sealed class Produto : SimpleEntity
    {
        public decimal Preco { get; private set; }
        public string? Descricao { get; private set; }

        public int CategoriaId { get; set; }
        public required Categoria Categoria { get; set; }

        public int? FotoPrincipalId { get; set; }
        public Foto? FotoPrincipal { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsRascunho { get; set; } = true;

        public Produto() { }

        public Produto(int id, string nome, decimal preco, string? descricao, int categoriaId, int? fotoPrincipalId, Foto? fotoPrincipal = null)
        {
            DomainValidationException.When(id < 0, "Valor de Id é inválido");

            Id = id;
            InitialValue(nome, preco, descricao, categoriaId, fotoPrincipalId, fotoPrincipal);
        }

        public Produto(string nome, decimal preco, string? descricao, int categoriaId, int? fotoPrincipalId, Foto? fotoPrincipal = null)
        {
            InitialValue(nome, preco, descricao, categoriaId, fotoPrincipalId, fotoPrincipal);
        }

       public void Update(string nome, decimal preco, string? descricao, int categoriaId)
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            CategoriaId = categoriaId;
            IsRascunho = false;
        }

        public void SetFotoPrincipal(Foto foto)
        {
            FotoPrincipal = foto;
            FotoPrincipalId = foto.Id;
            IsRascunho = false;
        }

        public void ToggleAcitve()
        {
            IsActive = !IsActive;
        }

        private void InitialValue(string nome, decimal preco, string? descricao, int categoriaId, int? fotoPrincipalId, Foto? fotoPrincipal = null)
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            CategoriaId = categoriaId;
            FotoPrincipalId = fotoPrincipalId;
            FotoPrincipal = fotoPrincipal;

            ValidateExceptions();
        }

        private void ValidateExceptions()
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