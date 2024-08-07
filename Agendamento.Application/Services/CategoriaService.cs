using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

public class CategoriaService : GenericService<Categoria, CategoriaDTO>, ICategoriaService
{
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository, IMapper mapper, IValidator<CategoriaDTO> validator)
        : base(categoriaRepository, mapper, validator)
    {
        _categoriaRepository = categoriaRepository;
    }

    public async Task UpdateStatusCategoriaAsync(int id, bool isActive)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        if (categoria == null)
            throw new ValidationException("Categoria não encontrada.");

        categoria.IsActive = isActive;
        await _categoriaRepository.UpdateAsync(categoria);
    }
}