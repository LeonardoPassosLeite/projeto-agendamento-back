using System.ComponentModel.DataAnnotations;
using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;

namespace Agendamento.Application.Services
{
    public class FotoService : IFotoService
    {
        private readonly IFotoRepository _fotoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public FotoService(IFotoRepository fotoRepository, IMapper mapper, IProdutoService produtoService)
        {
            _fotoRepository = fotoRepository ?? throw new ArgumentNullException(nameof(fotoRepository));
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<FotoDTO> AddFotoAsync(FotoDTO fotoDto)
        {
            if (fotoDto == null)
                throw new ValidationException("Foto não pode ser nula.");

            var fotoEntity = _mapper.Map<Foto>(fotoDto);

            if (fotoEntity.IsPrincial)
            {
                var fotoPrincipalExist = await _fotoRepository.FotoPrincipalExisteAsync(fotoEntity.ProdutoId);
                if (fotoPrincipalExist)
                    throw new DomainValidationException("O produto já possui uma foto principal.");
            }

            await _fotoRepository.AddAsync(fotoEntity);
            await _produtoService.AddFotoToProdutoAsync(fotoEntity.ProdutoId, _mapper.Map<FotoDTO>(fotoEntity));

            return _mapper.Map<FotoDTO>(fotoEntity);
        }

        public async Task<FotoDTO> GetFotoByIdAsync(int id)
        {
            if (id <= 0)
                throw new ValidationException("Id inválido.");

            var fotoEntity = await _fotoRepository.GetByIdAsync(id);
            if (fotoEntity == null)
                throw new NotFoundException($"Foto com Id {id} não encontrada.");

            return _mapper.Map<FotoDTO>(fotoEntity);
        }
    }
}