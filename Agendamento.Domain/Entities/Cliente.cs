using System.Text.RegularExpressions;
using Agendamento.Domain.Validation;

namespace Agendamento.Domain.Entities
{
    public sealed class Cliente : BaseInfosEntity
    {
        public string Cpf { get; private set; }
        public int Idade { get; private set; }

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public Cliente(string nome, string telefone, string cpf, int idade, string cep, string endereco, string cidade, string uf, string pais, string foto)
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

        public Cliente(int id, string nome, string telefone, string cpf, int idade, string cep, string endereco, string cidade, string uf, string pais, string foto)
        {
            DomainExceptionValidation.When(id < 0, "Valor de Id é inválido");

            Id = id;
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

        public void Update(string nome, string telefone, string cpf, int idade, string cep, string endereco, string cidade, string uf, string pais, string foto)
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
            DomainExceptionValidation.When(string.IsNullOrEmpty(Nome), "Nome é obrigatório");
            DomainExceptionValidation.When(Nome.Length < 3, "Nome deve ter no mínimo 3 caracteres");
            DomainExceptionValidation.When(Nome.Length > 100, "Nome deve ter no máximo 100 caracteres");

            DomainExceptionValidation.When(string.IsNullOrEmpty(Telefone), "Telefone é obrigatório");
            DomainExceptionValidation.When(Telefone.Length < 10 || Telefone.Length > 11, "Telefone deve ter 10 ou 11 dígitos");
            DomainExceptionValidation.When(!Regex.IsMatch(Telefone, @"^\d+$"), "Telefone deve conter apenas números");

            DomainExceptionValidation.When(Cpf.Length != 11 || !Regex.IsMatch(Cpf, @"^\d+$"), "CPF deve conter 11 dígitos numéricos");
            DomainExceptionValidation.When(Idade < 0 || Idade > 150, "Idade deve ser um valor entre 0 e 150");
            DomainExceptionValidation.When(Cep.Length != 8 || !Regex.IsMatch(Cep, @"^\d+$"), "CEP deve conter 8 dígitos numéricos");

            DomainExceptionValidation.When(string.IsNullOrEmpty(Endereco), "Endereço é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Cidade), "Ciadade é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Uf), "UF é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Pais), "País é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(Foto), "Foto é obrigatório");
        }
    }
}