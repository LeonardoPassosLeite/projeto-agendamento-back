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
            CreateMap<Produto, ProdutoDTO>()
                .ForMember(dest => dest.FotoPrincipal, opt => opt.Ignore())
                .ForMember(dest => dest.Fotos, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(dest => dest.FotoPrincipal, opt => opt.Ignore())
                .ForMember(dest => dest.Fotos, opt => opt.Ignore());
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<ClienteEmpresa, ClienteEmpresaDTO>().ReverseMap();
            CreateMap<EmpresaCliente, EmpresaClienteDTO>().ReverseMap();
        }
    }
}