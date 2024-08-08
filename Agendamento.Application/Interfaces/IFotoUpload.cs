using Microsoft.AspNetCore.Http;

namespace Agendamento.Application.Interfaces
{
    public interface IFotoUpload
    {
        public IFormFile? File { get; }
        public string? Url { get; }
    }
}