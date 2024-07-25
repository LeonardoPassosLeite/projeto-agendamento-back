using FluentValidation;
using Agendamento.Application.Helpers;

public class PaginationParamsValidator : AbstractValidator<PaginationParams>
{
    public PaginationParamsValidator()
    {
        RuleFor(x => x.Page).GreaterThan(0).WithMessage("O número da página deve ser maior que zero.");
        RuleFor(x => x.PageSize).GreaterThan(0).WithMessage("O tamanho da página deve ser maior que zero.");
    }
}