using System.Text.RegularExpressions;
using Agendamento.Domain.Exceptions;

namespace Agendamento.Domain.Entities
{
    public sealed class Cliente : BaseInfosEntity
    {
        public string Cpf { get; private set; } = string.Empty;
        public int Idade { get; private set; }


        public Cliente(string nome, string telefone, string cpf, int idade, string cep, string endereco, string cidade, string uf, string pais, string foto)
        {
            IniciaValor(nome, telefone, cpf, idade, cep, endereco, cidade, uf, pais, foto);
        }

        public Cliente(int id, string nome, string telefone, string cpf, int idade, string cep, string endereco, string cidade, string uf, string pais, string foto)
        {
            // ValidationException.When(id < 0, "Valor de Id é inválido");

            Id = id;
            IniciaValor(nome, telefone, cpf, idade, cep, endereco, cidade, uf, pais, foto);
        }

        public void Update(string nome, string telefone, string cpf, int idade, string cep, string endereco, string cidade, string uf, string pais, string foto)
        {
            IniciaValor(nome, telefone, cpf, idade, cep, endereco, cidade, uf, pais, foto);
        }

        private void IniciaValor(string nome, string telefone, string cpf, int idade, string cep, string endereco, string cidade, string uf, string pais, string foto)
        {
            Nome = nome;
            Telefone = telefone;
            Cpf = cpf;
            Idade = idade;
            Cep = cep;
            Endereco = endereco;
            Cidade = cidade;
            Uf = uf;
            Pais = pais;
            Foto = foto;

            ValidaExcessoes();
        }

        private void ValidaExcessoes()
        {
            // ValidationException.When(string.IsNullOrEmpty(Nome), "Nome é obrigatório");
            // ValidationException.When(Nome.Length < 3, "Nome deve ter no mínimo 3 caracteres");
            // ValidationException.When(Nome.Length > 100, "Nome deve ter no máximo 100 caracteres");

            // ValidationException.When(string.IsNullOrEmpty(Telefone), "Telefone é obrigatório");
            // ValidationException.When(Telefone.Length < 10 || Telefone.Length > 11, "Telefone deve ter 10 ou 11 dígitos");
            // ValidationException.When(!Regex.IsMatch(Telefone, @"^\d+$"), "Telefone deve conter apenas números");

            // ValidationException.When(Cpf.Length != 11 || !Regex.IsMatch(Cpf, @"^\d+$"), "CPF deve conter 11 dígitos numéricos");
            // ValidationException.When(Idade < 0 || Idade > 150, "Idade deve ser um valor entre 0 e 150");
            // ValidationException.When(Cep.Length != 8 || !Regex.IsMatch(Cep, @"^\d+$"), "CEP deve conter 8 dígitos numéricos");

            // ValidationException.When(string.IsNullOrEmpty(Endereco), "Endereço é obrigatório");
            // ValidationException.When(string.IsNullOrEmpty(Cidade), "Ciadade é obrigatório");
            // ValidationException.When(string.IsNullOrEmpty(Uf), "UF é obrigatório");
            // ValidationException.When(string.IsNullOrEmpty(Pais), "País é obrigatório");
            // ValidationException.When(Foto?.Length > 250, "Foto excede o número de caracteres permitidos");
        }
    }
}