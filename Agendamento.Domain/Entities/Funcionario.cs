using Agendamento.Domain.Entities;

namespace Agendamento.Domain.Enitiies
{
    public sealed class Funcionario : BaseInfosEntity
    {
        public string Cpf { get; set; }
        public bool IsVisit { get; set; } = true;

        public int EmpresaId { get; set; }
        public required Empresa Empresa { get; set; }


        public Funcionario() { }
    }
}