using FluentValidation;
using Agendamento.Application.DTOs;

namespace Agendamento.Application.Validators
{
    public class ProdutoDTOValidator : AbstractValidator<ProdutoDTO>
    {
        public ProdutoDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome é obrigatório")
                .MinimumLength(3).WithMessage("Nome deve ter no mínimo 3 caracteres")
                .MaximumLength(100).WithMessage("Nome deve ter no máximo 100 caracteres");

            RuleFor(x => x.Preco)
                .GreaterThanOrEqualTo(0).WithMessage("Valor de preço inválido");

            RuleFor(x => x.Descricao)
                .NotEmpty().WithMessage("Descrição é obrigatória")
                .MinimumLength(3).WithMessage("Descrição deve ter no mínimo 3 caracteres")
                .MaximumLength(100).WithMessage("Descrição deve ter no máximo 100 caracteres");

            RuleFor(x => x.FotoPrincipal)
                .MaximumLength(250).WithMessage("Foto principal excede o número de caracteres permitidos");

            RuleForEach(x => x.Fotos)
                .MaximumLength(250).WithMessage("Uma ou mais fotos excedem o número de caracteres permitidos");
        }
    }
}
