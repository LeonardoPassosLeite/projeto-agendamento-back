namespace Agendamento.Domain.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
        public DateTime DataAtualizacao { get; set; } = DateTime.UtcNow;

    }
    public class SimpleEntity : BaseEntity
    {
        public string Nome { get; set; } = string.Empty;
    }

    public class BaseInfosEntity : SimpleEntity
    {
        public string Telefone { get;  set; } = string.Empty;
        public string Cep { get;  set; } = string.Empty;
        public string Endereco { get;  set; } = string.Empty;
        public string Cidade { get;  set; } = string.Empty;
        public string Uf { get;  set; } = string.Empty;
        public string Pais { get;  set; } = string.Empty;
        public int? FotoPrincipalId { get; set; }
        public Foto? FotoPrincipal { get; set; }
    }
}