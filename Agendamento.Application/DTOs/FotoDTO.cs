using Microsoft.AspNetCore.Http;

namespace Agendamento.Application.DTOs
{
    public class FotoBaseDTO
    {
        public string? Url { get; set; }
        public string? FilePath { get; set; }
        public int ProdutoId { get; set; }
        public IFormFile? File { get; set; }
    }
    public class FotoDTO : FotoBaseDTO
    {
        public int Id { get; set; }
    }

    public class FotoUploadDTO : FotoBaseDTO
    { }
}