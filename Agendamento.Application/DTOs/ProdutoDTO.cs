using Agendamento.Application.DTOs.Commons;

namespace Agendamento.Application.DTOs
{
    public class ProdutoDTO : BaseDTO
    {
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
    }

    public class ProdutoFotoDTO : ProdutoDTO
    {
        public FotoDTO? FotoPrincipal { get; set; }
        public bool IsActive { get; set; }
        public bool IsRascunho { get; set; }
    }
}