using Agendamento.Application.DTOs;
using Agendamento.Application.Helpers;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;

public class GetPagedProdutos
{
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<PaginationParams> _validator;

    public GetPagedProdutos(IProdutoRepository produtoRepository, IMapper mapper, IValidator<PaginationParams> validator)
    {
        _produtoRepository = produtoRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<PagedResultDTO<ProdutoFotoDTO>> ExecuteAsync(PaginationParams paginationParams)
    {
        if (paginationParams == null)
            throw new ValidationException("Parâmetros de paginação não podem ser nulos.");

        ValidationResult validationResult = await _validator.ValidateAsync(paginationParams);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var pagedResult = await _produtoRepository.GetPagedAsync(
            page: paginationParams.Page,
            pageSize: paginationParams.PageSize,
            filterText: paginationParams.Filter
        );

        var produtoDtos = _mapper.Map<IEnumerable<ProdutoFotoDTO>>(pagedResult.Items);

        return new PagedResultDTO<ProdutoFotoDTO>(produtoDtos, pagedResult.TotalCount);
    }
}