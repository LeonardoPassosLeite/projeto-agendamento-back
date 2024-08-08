using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Application.Services.Commons;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Configuration;

public class FotoClienteService : FotoServiceBase<FotoCliente, FotoClienteDTO>, IFotoClienteService
{
    private readonly IClienteRepository _clienteRepository;

    public FotoClienteService(IConfiguration configuration,
                              IFotoClienteRepository fotoRepository,
                              IClienteRepository clienteRepository,
                              IMapper mapper,
                              IValidator<FotoClienteDTO> validator)
        : base(configuration, fotoRepository, mapper, validator)
    {
        _clienteRepository = clienteRepository ?? throw new ArgumentNullException(nameof(clienteRepository));
    }

    public async Task<FotoClienteDTO> UploadFileAsync(FotoClienteDTO fotoClienteUploadDto)
    {
        return await base.UploadFileAsync(fotoClienteUploadDto, async (foto) =>
        {
            var cliente = await _clienteRepository.GetByIdAsync(fotoClienteUploadDto.ClienteId);
            if (cliente == null)
                throw new NotFoundException($"Cliente com Id {fotoClienteUploadDto.ClienteId} não encontrado.");

            // Adicione lógica adicional necessária para associar a foto ao cliente, se necessário.
            // Exemplo: cliente.SetFotoPrincipal(foto);
            // await _clienteRepository.UpdateAsync(cliente);
        });
    }
}
