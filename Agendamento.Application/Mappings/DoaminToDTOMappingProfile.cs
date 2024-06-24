using Agendamento.Application.DTOs;
using Agendamento.Domain.Entities;
using AutoMapper;

namespace Agendamento.Application.Mappings
{
    public class DoaminToDTOMappingProfile : Profile
    {
        public DoaminToDTOMappingProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<ClienteEmpresa, ClienteEmpresaDTO>().ReverseMap();
            CreateMap<EmpresaCliente, EmpresaClienteDTO>().ReverseMap();
        }
    }
}