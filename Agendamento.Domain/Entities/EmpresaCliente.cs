namespace Agendamento.Domain.Entities
{
    public class EmpresaCliente : BaseInfosEntity
    {
        public string Cnpj { get; private set; }
        public ICollection<ClienteEmpresa> ClienteEmpresas { get; set; } = new List<ClienteEmpresa>();
    }
}