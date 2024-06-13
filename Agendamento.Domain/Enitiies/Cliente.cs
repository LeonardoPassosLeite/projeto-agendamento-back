using System.Text.RegularExpressions;
using Agendamento.Domain.Validation;

namespace Agendamento.Domain.Enitiies
{
    public sealed class Cliente : BaseInfosEntity
    {
        public string Cpf { get; private set; }
        public int Idade { get; private set; }

        public ICollection<Produto> Produtos { get; set; }

        public Cliente(string nome, string telefone, string cpf, int idade, string cep, string endereco, string cidade, string uf, string pais, string foto)
        {
            ValidaExcessoes(nome, telefone, cpf, idade, cep, endereco, cidade, uf, pais, foto);
        }

        public Cliente(int id, string nome, string telefone, string cpf, int idade, string cep, string endereco, string cidade, string uf, string pais, string foto)
        {
            DomainExceptionValidation.When(id < 0, "Valor de Id é inválido");

            Id = id;
            ValidaExcessoes(nome, telefone, cpf, idade, cep, endereco, cidade, uf, pais, foto);
        }

        public void Update(string nome, string telefone, string cpf, int idade, string cep, string endereco, string cidade, string uf, string pais, string foto)
        {
            ValidaExcessoes(nome, telefone, cpf, idade, cep, endereco, cidade, uf, pais, foto);
        }

        private void ValidaExcessoes(string nome, string telefone, string cpf, int idade, string cep, string endereco, string cidade, string uf, string pais, string foto)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(nome), "Nome é obrigatório");
            DomainExceptionValidation.When(nome.Length < 3, "Nome deve ter no mínimo 3 caracteres");
            DomainExceptionValidation.When(nome.Length > 100, "Nome deve ter no máximo 100 caracteres");

            DomainExceptionValidation.When(string.IsNullOrEmpty(telefone), "Telefone é obrigatório");
            DomainExceptionValidation.When(telefone.Length < 10 || telefone.Length > 11, "Telefone deve ter 10 ou 11 dígitos");
            DomainExceptionValidation.When(!Regex.IsMatch(telefone, @"^\d+$"), "Telefone deve conter apenas números");

            DomainExceptionValidation.When(cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d+$"), "CPF deve conter 11 dígitos numéricos");
            DomainExceptionValidation.When(idade < 0 || idade > 150, "Idade deve ser um valor entre 0 e 150");
            DomainExceptionValidation.When(cep.Length != 8 || !Regex.IsMatch(cep, @"^\d+$"), "CEP deve conter 8 dígitos numéricos");

            DomainExceptionValidation.When(string.IsNullOrEmpty(endereco), "Endereço é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(cidade), "Ciadade é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(uf), "UF é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(pais), "País é obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(foto), "Foto é obrigatório");

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
        }
    }
}