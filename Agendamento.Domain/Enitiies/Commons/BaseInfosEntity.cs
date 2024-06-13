namespace Agendamento.Domain.Enitiies
{
    public class BaseInfosEntity
    {
        public int Id { get; protected set; }
        public string Nome { get; protected set; }
        public string Telefone { get; protected set; }
        public string Cep { get; protected set; }
        public string Endereco { get; protected set; }
        public string Cidade { get; protected set; }
        public string Uf { get; protected set; }
        public string Pais { get; protected set; }
        public string Foto { get; protected set; }
    }
}