using Agendamento.Domain.Exceptions;

namespace Agendamento.Domain.Entities
{
    public sealed class Foto : BaseEntity
    {
        public string? Url { get; private set; }
        public string? FilePath { get; private set; }
        public bool IsPrincial { get; private set; } = true;
        public int ProdutoId { get; set; }
        public required Produto Produto { get; set; }

        public Foto(string? url, string? filePath, int produtoId)
        {
            Url = url;
            FilePath = filePath;
            ProdutoId = produtoId;

            ValidaExcessoes();
        }

        public Foto(int id, string? url, string? filePath, int produtoId)
        {
            DomainValidationException.When(id < 0, "Valor de Id é inválido");

            Id = id;
            Url = url;
            FilePath = filePath;
            ProdutoId = produtoId;

            ValidaExcessoes();
        }

        public void SetPrincipal(bool isPrincial)
        {
            IsPrincial = isPrincial;
        }

        private void ValidaExcessoes()
        {
            DomainValidationException.When(string.IsNullOrEmpty(Url) && string.IsNullOrEmpty(FilePath), "URL ou Caminho do arquivo devem ser fornecidos");
            DomainValidationException.When(!string.IsNullOrEmpty(Url) && Url.Length < 5, "URL deve ter no mínimo 5 caracteres");
            DomainValidationException.When(ProdutoId <= 0, "ProdutoId é obrigatório e deve ser maior que zero");
        }
    }
}