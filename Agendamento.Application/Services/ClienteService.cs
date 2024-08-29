using Agendamento.Application.DTOs;
using Agendamento.Application.Helpers;
using Agendamento.Application.Interfaces;
using Agendamento.Application.UseCases.Customs;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace Agendamento.Application.Services
{
    public class ClienteService : CustomService<Cliente, ClienteDTO, ClienteFotoDTO>, IClienteService
    {
        private readonly GetPagedUseCase<Cliente, ClienteFotoDTO> _getPagedUseCase;
        private readonly UpdateCustomUseCase<Cliente, ClienteDTO, ClienteFotoDTO> _updateProdutoUseCase;

        public ClienteService(
            IClienteRepository clienteRepository,
            IMapper mapper,
            IValidator<ClienteDTO> addValidator,
            IValidator<ClienteFotoDTO> validator)
            : base(clienteRepository, mapper, addValidator, validator)
        {
            _getPagedUseCase = new GetPagedUseCase<Cliente, ClienteFotoDTO>(clienteRepository, mapper);
            _updateProdutoUseCase = new UpdateCustomUseCase<Cliente, ClienteDTO, ClienteFotoDTO>(clienteRepository, mapper, addValidator);
        }

        public override async Task<PagedResultDTO<ClienteFotoDTO>> GetPagedAsync(PaginationParams paginationParams)
        {
            return await _getPagedUseCase.ExecuteAsync(paginationParams, c => c.FotoPrincipal!);
        }

        public override async Task<ClienteFotoDTO> UpdateCustomAsync(ClienteDTO dto)
        {
            return await _updateProdutoUseCase.ExecuteAsync(dto);
        }
    }
}