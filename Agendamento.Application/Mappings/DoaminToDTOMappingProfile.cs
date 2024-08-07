using Agendamento.Application.DTOs;
using Agendamento.Domain.Enitiies;
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
            CreateMap<Cliente, ClienteFotoDTO>().ReverseMap()
                .ForMember(dest => dest.FotoPrincipal, opt => opt.MapFrom(src => src.FotoPrincipal));
            CreateMap<Funcionario, FuncionarioDTO>().ReverseMap();
            CreateMap<Foto, FotoDTO>().ReverseMap();
            CreateMap<Foto, FotoUploadDTO>().ReverseMap();
        }
    }
}
