using Agendamento.Application.DTOs;
using Agendamento.Application.Helpers;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;

namespace Agendamento.Application.Services
{
    public class ClienteService : CustomService<Cliente, ClienteDTO, ClienteFotoDTO>, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository, IMapper mapper, IValidator<ClienteDTO> addValidator)
            : base(clienteRepository, mapper, addValidator)
        {
            _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
        }

        public override async Task<PagedResultDTO<ClienteFotoDTO>> GetPagedAsync(PaginationParams paginationParams)
        {
            if (paginationParams == null)
                throw new ValidationException("Parâmetros de paginação não podem ser nulos.");

            var pagedResult = await _clienteRepository.GetPagedAsync(
                filter: null,
                page: paginationParams.Page,
                pageSize: paginationParams.PageSize,
                filterText: paginationParams.Filter,
                c => c.FotoPrincipal! 
            );

            var itemsDto = _mapper.Map<IEnumerable<ClienteFotoDTO>>(pagedResult.Items);

            return new PagedResultDTO<ClienteFotoDTO>(itemsDto, pagedResult.TotalCount);
        }
    }
}