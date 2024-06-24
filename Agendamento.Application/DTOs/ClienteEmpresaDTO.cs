namespace Agendamento.Application.DTOs
{
    public class ClienteEmpresaDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Foto { get; set; }
        public int EmpresaId { get; set; }
    }
}
