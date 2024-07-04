using Agendamento.Application.DTOs.Commons;
using Microsoft.AspNetCore.Http;

namespace Agendamento.Application.DTOs
{
    public class ProdutoDTO : BaseDTO
    {
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public IFormFile FotoPrincipal { get; set; }
        public List<IFormFile> Fotos { get; set; } = new List<IFormFile>();
        public bool IsActive { get; set; } = true;

        public int CategoriaId { get; set; }
    }
}
