using Agendamento.Application.DTOs;
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
    }
}