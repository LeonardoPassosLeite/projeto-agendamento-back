using FluentValidation;
using Agendamento.Application.DTOs;

namespace Agendamento.Application.Validators
{
    public class ClienteEmpresaDTOValidator : AbstractValidator<ClienteEmpresaDTO>
    {
        public ClienteEmpresaDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Telefone)
                .NotEmpty().WithMessage("Telefone é obrigatório")
                .Length(10, 11).WithMessage("Telefone deve ter 10 ou 11 dígitos")
                .Matches(@"^\d+$").WithMessage("Telefone deve conter apenas números");

            RuleFor(x => x.Foto)
                .NotEmpty().WithMessage("Foto é obrigatória");

            RuleFor(x => x.EmpresaId)
                .GreaterThan(0).WithMessage("EmpresaId deve ser um valor positivo");
        }
    }
}
