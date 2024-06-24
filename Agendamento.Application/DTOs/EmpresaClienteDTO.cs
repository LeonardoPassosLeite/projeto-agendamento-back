namespace Agendamento.Application.DTOs
{
    public class EmpresaClienteDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Cnpj { get; set; }
        public ICollection<ClienteEmpresaDTO> ClienteEmpresas { get; set; } = new List<ClienteEmpresaDTO>();
    }
}
