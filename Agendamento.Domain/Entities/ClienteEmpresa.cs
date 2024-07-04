using System.Text.RegularExpressions;
using Agendamento.Domain.Exceptions;

namespace Agendamento.Domain.Entities
{
    public sealed class ClienteEmpresa : BaseEntity
    {
        public string Telefone { get; private set; }
        public string Cpf { get; private set; }
        public string CargoCliente { get; private set; }
        public string Foto { get; private set; }
        public int EmpresaId { get; set; }
        public EmpresaCliente EmpresaCliente { get; set; }


        public ClienteEmpresa(string nome, string telefone, string cpf, string cargoCliente, string foto)
        {
            ValidaExcessoes(nome, telefone, cpf, cargoCliente, foto);
        }

        public ClienteEmpresa(int id, string nome, string telefone, string cpf, string cargoCliente, string foto)
        {
            // ValidationException.When(id < 0, "Valor de Id é inválido");

            Id = id;
            ValidaExcessoes(nome, telefone, cpf, cargoCliente, foto);
        }

        public void Update(string nome, string telefone, string cpf, string cargoCliente, string foto)
        {
            ValidaExcessoes(nome, telefone, cpf, cargoCliente, foto);
        }

        private void ValidaExcessoes(string nome, string telefone, string cpf, string cargoCliente, string foto)
        {
            // ValidationException.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");
            // ValidationException.When(nome.Length < 3, "Nome deve ter no mínimo 3 caracteres");
            // ValidationException.When(nome.Length > 100, "Nome deve ter no máximo 100 caracteres");

            // ValidationException.When(string.IsNullOrEmpty(telefone), "Telefone é obrigatório");
            // ValidationException.When(telefone.Length < 10 || telefone.Length > 11, "Telefone deve ter 10 ou 11 dígitos");
            // ValidationException.When(cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d+$"), "CPF deve conter 11 dígitos numéricos");

            // ValidationException.When(string.IsNullOrEmpty(cargoCliente), "Cargo é obrigatório");
            // ValidationException.When(string.IsNullOrEmpty(foto), "Foto é obrigatório");

            Nome = nome;
            Telefone = telefone;
            Cpf = cpf;
            CargoCliente = cargoCliente;
            Foto = foto;
        }
    }
}