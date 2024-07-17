using Agendamento.Application.DTOs.Commons;

namespace Agendamento.Application.DTOs
{
    public class CategoriaDTO : BaseDTO
    { }

    public class CategoriaActiveDTO : CategoriaDTO
    {
        public bool isActive { get; set; }
    }
}