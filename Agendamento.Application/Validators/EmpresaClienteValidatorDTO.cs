using FluentValidation;
using Agendamento.Application.DTOs;

namespace Agendamento.Application.Validators
{
    public class EmpresaClienteDTOValidator : AbstractValidator<EmpresaClienteDTO>
    {
        public EmpresaClienteDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Cnpj)
                .NotEmpty().WithMessage("CNPJ é obrigatório")
                .Length(14).WithMessage("CNPJ deve conter 14 dígitos")
                .Matches(@"^\d+$").WithMessage("CNPJ deve conter apenas números");

            RuleForEach(x => x.ClienteEmpresas).SetValidator(new ClienteEmpresaDTOValidator());
        }
    }
}
