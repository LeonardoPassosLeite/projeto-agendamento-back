namespace Agendamento.Domain.Enitiies
{
    public class EmpresaCliente : BaseInfosEntity
    {
        public string Cnpj { get; private set; }
        public ICollection<ClienteEmpresa> ClienteEmpresas { get; set; }
    }
}