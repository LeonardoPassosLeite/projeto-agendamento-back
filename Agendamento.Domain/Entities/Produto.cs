using Agendamento.Domain.Enitiies;

namespace Agendamento.Domain.Entities
{
    public sealed class Produto : SimpleEntity
    {
        public decimal Preco { get; set; }
        public string Marca { get; set; }
        public string Localidade { get; set; }
        public string? Descricao { get; set; }
        public int Ano { get; set; }
        public int Quilometragem { get; set; }

        public int? FotoPrincipalId { get; set; }
        public FotoProduto? FotoPrincipal { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsRascunho { get; set; } = true;

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
        public ICollection<Empresa> Empresa { get; set; } = new List<Empresa>();

        public Produto() { }

        public void Update(string nome, decimal preco, string? descricao, int categoriaId)
        {
            Nome = nome;
            Preco = preco;
            Descricao = descricao;
            CategoriaId = categoriaId;
            IsRascunho = false;
        }

        public void SetFotoPrincipal(FotoProduto fotoProduto)
        {
            FotoPrincipal = fotoProduto;
            FotoPrincipalId = fotoProduto.Id;
            IsRascunho = false;
        }

        public void ToggleAcitve()
        {
            IsActive = !IsActive;
        }
    }
}