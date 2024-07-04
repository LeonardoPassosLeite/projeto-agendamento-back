namespace Agendamento.Application.DTOs.Commons
{
    public class BaseInfosDTO
    {
        public int Id { get; protected set; }
        public string Nome { get; protected set; } = string.Empty;
        public string Telefone { get; protected set; } = string.Empty;
        public string Cep { get; protected set; } = string.Empty;
        public string Endereco { get; protected set; } = string.Empty;
        public string Cidade { get; protected set; } = string.Empty;
        public string Uf { get; protected set; } = string.Empty;
        public string Pais { get; protected set; } = string.Empty;
        public string? Foto { get; protected set; }
    }
}