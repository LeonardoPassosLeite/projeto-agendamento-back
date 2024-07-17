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

            var produto = await _produtoService.GetByIdAsync(fotoDto.ProdutoId);
            if (produto == null)
                throw new NotFoundException($"Produto com Id {fotoDto.ProdutoId} não encontrado.");

            var fotoEntity = _mapper.Map<Foto>(fotoDto);

            if (fotoEntity.IsPrincipal)
            {
                var fotoPrincipalExist = await _fotoRepository.FotoPrincipalExistAsync(fotoEntity.ProdutoId);
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

        public async Task<FotoDTO> UpdateFotoAsync(FotoDTO fotoDto)
        {
            if (fotoDto == null)
                throw new ValidationException("Foto não pode ser nulo");

            try
            {
                var foto = await _fotoRepository.GetByIdAsync(fotoDto.Id);
                if (foto == null)
                    throw new NotFoundException($"Foto com Id {fotoDto.Id} não encontrada");

                fotoDto.ProdutoId = foto.ProdutoId;

                _mapper.Map(fotoDto, foto);

                var updatedEntity = await _fotoRepository.UpdateAsync(foto);
                if (updatedEntity == null)
                    throw new NotFoundException($"Foto com Id {fotoDto.Id} não encontrada");

                return _mapper.Map<FotoDTO>(updatedEntity);
            }
            catch (DatabaseException ex)
            {
                throw new ApplicationException("Ocorreu um erro ao atualizar a foto", ex);
            }
        }

    }
}