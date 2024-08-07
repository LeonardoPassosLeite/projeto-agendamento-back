using Agendamento.Domain.Enitiies;

namespace Agendamento.Domain.Entities
{
    public class Empresa : BaseInfosEntity
    {
        public string Cnpj { get; protected set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
        // public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Empresa() { }

        public void Disable()
        {
            IsActive = false;
        }
    }
}