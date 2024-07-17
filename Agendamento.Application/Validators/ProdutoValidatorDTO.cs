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
                .MinimumLength(3).When(x => !string.IsNullOrEmpty(x.Descricao)).WithMessage("Descrição deve ter no mínimo 3 caracteres")
                .MaximumLength(100).WithMessage("Descrição deve ter no máximo 100 caracteres");

            RuleFor(x => x.CategoriaId)
                .GreaterThan(0).WithMessage("CategoriaId é obrigatório e deve ser maior que zero");
        }
    }
}