namespace Agendamento.Domain.Entities
{
    public sealed class Cliente : BaseInfosEntity
    {
        public string Empresa { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public int Idade { get; set; }
        public bool IsVisit { get; set; } = false;

        public int? FotoPrincipalId { get; set; }
        public FotoCliente? FotoPrincipal { get; set; }

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Cliente() { }
    }
}