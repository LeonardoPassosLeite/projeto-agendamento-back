using Agendamento.Domain.Exceptions;

namespace Agendamento.Domain.Entities
{
    public sealed class Foto : BaseEntity
    {
        public string? Url { get; set; }
        public string? FilePath { get; set; }
        public bool IsPrincipal { get; private set; } = true;
        public int ProdutoId { get; set; }
        public required Produto Produto { get; set; }

        public Foto(string? url, string? filePath, int produtoId)
        {
            Url = url;
            FilePath = filePath;
            ProdutoId = produtoId;

            ValidateExceptions();
        }

        public Foto(int id, string? url, string? filePath, int produtoId)
        {
            DomainValidationException.When(id < 0, "Valor de Id é inválido");

            Id = id;
            Url = url;
            FilePath = filePath;
            ProdutoId = produtoId;

            ValidateExceptions();
        }

        public void SetPrincipal(bool isPrincipal)
        {
            IsPrincipal = isPrincipal;
        }

        private void ValidateExceptions()
        {
            DomainValidationException.When(string.IsNullOrEmpty(Url) && string.IsNullOrEmpty(FilePath), "URL ou Caminho do arquivo devem ser fornecidos");
            DomainValidationException.When(!string.IsNullOrEmpty(Url) && Url.Length < 5, "URL deve ter no mínimo 5 caracteres");
            DomainValidationException.When(ProdutoId <= 0, "ProdutoId é obrigatório e deve ser maior que zero");
        }
    }
}