using FluentValidation;
using Agendamento.Application.DTOs;

namespace Agendamento.Application.Validators
{
    public class ClienteDTOValidator : AbstractValidator<ClienteDTO>
    {
        public ClienteDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório")
                .Length(10, 11).WithMessage("Telefone deve ter 10 ou 11 dígitos")
                .Matches(@"^\d+$").WithMessage("Telefone deve conter apenas números");

            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("CEP é obrigatório")
                .Length(8).WithMessage("CEP deve conter 8 dígitos numéricos")
                .Matches(@"^\d+$").WithMessage("CEP deve conter apenas números");

            RuleFor(x => x.Endereco)
                .NotEmpty().WithMessage("Endereço é obrigatório");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("Cidade é obrigatória");

            RuleFor(x => x.Foto)
                .MaximumLength(250).WithMessage("Foto excede o número de caracteres permitidos");
        }
    }
}
