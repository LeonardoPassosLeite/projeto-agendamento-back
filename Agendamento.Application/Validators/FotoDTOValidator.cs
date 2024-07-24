using Agendamento.Application.DTOs;
using FluentValidation;

public class FotoDTOValidator : AbstractValidator<FotoDTO>
{
    public FotoDTOValidator()
    {
        RuleFor(x => x.ProdutoId).GreaterThan(0).WithMessage("ProdutoId é obrigatório e deve ser maior que zero.");
        RuleFor(x => x.File).NotNull().WithMessage("Arquivo é obrigatório.");
    }
}