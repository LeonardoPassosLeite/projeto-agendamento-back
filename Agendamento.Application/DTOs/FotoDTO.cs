namespace Agendamento.Application.DTOs
{
    public class FotoDTO
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? FilePath { get; set; }
        public bool IsPrincipal { get; set; }
        public int ProdutoId { get; set; }
    }
}