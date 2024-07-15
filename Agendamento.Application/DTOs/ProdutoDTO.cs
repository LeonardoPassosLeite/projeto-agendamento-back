using Agendamento.Application.DTOs.Commons;

namespace Agendamento.Application.DTOs
{
    public class ProdutoBaseDTO : BaseDTO
    {
        public decimal Preco { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public int CategoriaId { get; set; }
    }

    public class ProdutoDTO : ProdutoBaseDTO
    {
        public bool IsActive { get; set; } = true;
        public bool IsRascunho { get; set; } = true;
    }

    public class ProdutoFotoDTO : ProdutoDTO
    {
        public FotoDTO? FotoPrincipal { get; set; }
    }

    public class ProdutoUpdateDTO : ProdutoBaseDTO
    { }
}