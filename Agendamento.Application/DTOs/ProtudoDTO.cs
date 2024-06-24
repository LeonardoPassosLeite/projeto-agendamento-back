namespace Agendamento.Application.DTOs
{
    public class ProdutoDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public string Descricao { get; set; }
        public string? FotoPrincipal { get; set; }
        public List<string> Fotos { get; set; }
        public bool IsActive { get; set; } = true;

        public int CategoriaId { get; set; }
    }
}
