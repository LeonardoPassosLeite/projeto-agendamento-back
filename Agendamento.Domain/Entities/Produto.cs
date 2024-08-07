using Agendamento.Domain.Enitiies;

namespace Agendamento.Domain.Entities
{
    public sealed class Produto : SimpleEntity
    {
        public decimal Preco { get; set; }
        public string? Descricao { get; set; }
        public int? FotoPrincipalId { get; set; }
        public Foto? FotoPrincipal { get; set; }

        public bool IsActive { get; set; } = true;
        public bool IsRascunho { get; set; } = true;

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public int? ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public int? FuncionarioId { get; set; }
        public Funcionario? Funcionario { get; set; }

        public Produto() { }

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
    }
}