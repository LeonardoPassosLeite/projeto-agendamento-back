using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;

namespace Agendamento.Domain.Entities.Commons
{
    public abstract class FotoBase : IFoto
    {
        public int Id { get; set; }
        public string? Url { get; set; }
        public string? FilePath { get; set; }
        public bool IsPrincipal { get; set; } = true;

        public void SetId(int id)
        {
            Id = id;
        }

        public void SetFileProperties(string url, string filePath, bool isPrincipal = true)
        {
            Url = url;
            FilePath = filePath;
            IsPrincipal = isPrincipal;
        }

        protected void ValidateCommonExceptions()
        {
            DomainValidationException.When(!string.IsNullOrEmpty(Url) && Url.Length < 5, "URL deve ter no mínimo 5 caracteres");
        }
    }
}