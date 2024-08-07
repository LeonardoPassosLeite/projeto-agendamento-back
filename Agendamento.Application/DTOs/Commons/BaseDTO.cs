namespace Agendamento.Application.DTOs.Commons
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }

    public class BaseInfosDTO : BaseDTO
    {
        public string Telefone { get; set; } = string.Empty;
        public string Cep { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Uf { get; set; } = string.Empty;
        public string Pais { get; set; } = string.Empty;
    }
}