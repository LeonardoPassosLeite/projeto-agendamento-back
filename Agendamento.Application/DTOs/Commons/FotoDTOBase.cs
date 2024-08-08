using Agendamento.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Agendamento.Application.DTOs.Commons
{
    public abstract class FotoDTOBase : IFotoUpload
    {
        public IFormFile? File { get; set; }
        public string? Url { get; set; }
    }
}