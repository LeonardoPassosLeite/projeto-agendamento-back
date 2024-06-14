using Agendamento.Domain.Validation;

namespace Agendamento.Domain.Entities
{
    public sealed class Produto : BaseEntity
    {
        public decimal Preco { get; private set; }
        public string Descricao { get; private set; }
        public string? FotoPrincipal { get; private set; }
        public List<string> Fotos { get; private set; }
        public bool IsActive { get; set; } = true;

        public int CategoriaId { get; set; }
        public required Categoria Categoria { get; set; }
        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public Produto(string nome, decimal preco, string descricao, string fotoPrincipal, List<string> fotos)
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            FotoPrincipal = fotoPrincipal;
            Fotos = fotos ?? new List<string>();

            ValidaExcessoes();
        }

        public Produto(int id, string nome, decimal preco, string descricao, string fotoPrincipal, List<string> fotos)
        {
            DomainExceptionValidation.When(id < 0, "Valor de Id é inválido");

            Id = id;
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            FotoPrincipal = fotoPrincipal;
            Fotos = fotos ?? new List<string>();

            ValidaExcessoes();
        }

        public void Update(string nome, decimal preco, string descricao, int categoriaId, string fotoPrincipal, List<string> fotos)
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            FotoPrincipal = fotoPrincipal;
            Fotos = fotos ?? new List<string>();
            CategoriaId = categoriaId;

            ValidaExcessoes();
        }

        public void Deactivate()
        {
            IsActive = false;
        }

        private void ValidaExcessoes()
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(Nome), "Nome é obrigatório");
            DomainExceptionValidation.When(Nome.Length < 3, "Nome deve ter no mínimo 3 caracteres");
            DomainExceptionValidation.When(Nome.Length > 100, "Nome deve ter no máximo 100 caracteres");

            DomainExceptionValidation.When(Preco < 0, "Valor de preço Inválido");

            DomainExceptionValidation.When(Descricao.Length < 3, "Descrição deve ter no mínimo 3 caracteres");
            DomainExceptionValidation.When(Descricao.Length > 100, "Descrição deve ter no máximo 100 caracteres");

            DomainExceptionValidation.When(FotoPrincipal?.Length > 250, "Foto principal excede o número de caracteres permitidos");
            DomainExceptionValidation.When(Fotos?.Any(foto => foto.Length > 250) == true, "Uma ou mais fotos excedem o número de caracteres permitidos");
        }
    }
}