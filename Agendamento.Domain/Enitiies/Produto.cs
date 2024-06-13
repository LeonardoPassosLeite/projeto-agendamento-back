using Agendamento.Domain.Validation;

namespace Agendamento.Domain.Enitiies
{
    public sealed class Produto : BaseEntity
    {
        public decimal Preco { get; private set; }
        public string Descricao { get; private set; }
        public string FotoPrincipal { get; private set; }
        public List<string> Fotos { get; private set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }

        public Produto(string nome, decimal preco, string descricao, string fotoPrincipal = null, List<string> fotos = null)
        {
            ValidaExcessoes(nome, preco, descricao, fotoPrincipal, fotos);
        }

        public Produto(int id, string nome, decimal preco, string descricao, string fotoPrincipal = null, List<string> fotos = null)
        {
            DomainExceptionValidation.When(id < 0, "Valor de Id é inválido");

            Id = id;
            ValidaExcessoes(nome, preco, descricao, fotoPrincipal, fotos);
        }

        public void Update(string nome, decimal preco, string descricao, int categoriaId, string fotoPrincipal = null, List<string> fotos = null)
        {
            ValidaExcessoes(nome, preco, descricao, fotoPrincipal, fotos);
            CategoriaId = categoriaId;
        }

        private void ValidaExcessoes(string nome, decimal preco, string descricao, string fotoPrincipal, List<string> fotos)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");
            DomainExceptionValidation.When(nome.Length < 3, "Nome deve ter no mínimo 3 caracteres");
            DomainExceptionValidation.When(nome.Length > 100, "Nome deve ter no máximo 100 caracteres");

            DomainExceptionValidation.When(preco < 0, "Valor de preço Inválido");
            
            DomainExceptionValidation.When(descricao.Length < 3, "Descrição deve ter no mínimo 3 caracteres");
            DomainExceptionValidation.When(descricao.Length > 100, "Descrição deve ter no máximo 100 caracteres");

            if (!string.IsNullOrEmpty(fotoPrincipal))
            {
                DomainExceptionValidation.When(fotoPrincipal.Length > 250, "Foto principal excede o número de caracteres permitidos");
            }

            if (fotos != null && fotos.Any())
            {
                DomainExceptionValidation.When(fotos.Any(foto => foto.Length > 250), "Uma ou mais fotos excedem o número de caracteres permitidos");
            }

            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            FotoPrincipal = fotoPrincipal;
            Fotos = fotos ?? new List<string>();
        }
    }
}