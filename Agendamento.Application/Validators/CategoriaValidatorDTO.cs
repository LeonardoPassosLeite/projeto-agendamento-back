using FluentValidation;
using Agendamento.Application.DTOs;

namespace Agendamento.Application.Validators
{
    public class CategoriaDTOValidator : AbstractValidator<CategoriaDTO>
    {
        public CategoriaDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O Nome é obrigatório")
                .MinimumLength(3).WithMessage("O Nome deve ter no mínimo 3 caracteres")
                .MaximumLength(100).WithMessage("O Nome deve ter no máximo 100 caracteres");
        }
    }
}