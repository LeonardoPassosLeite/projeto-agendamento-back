using Agendamento.Application.DTOs;
using Agendamento.Domain.Entities;
using AutoMapper;

namespace Agendamento.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Produto, ProdutoFotoDTO>()
                .ForMember(dest => dest.FotoPrincipal, opt => opt.MapFrom(src => src.FotoPrincipal));
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            CreateMap<ClienteEmpresa, ClienteEmpresaDTO>().ReverseMap();
            CreateMap<EmpresaCliente, EmpresaClienteDTO>().ReverseMap();
            CreateMap<Foto, FotoDTO>().ReverseMap();
            CreateMap<Foto, FotoUploadDTO>().ReverseMap();
        }
    }
}
