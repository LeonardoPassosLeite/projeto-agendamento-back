using Agendamento.Application.DTOs.Commons;

namespace Agendamento.Application.DTOs
{
    public class CategoriaDTO : BaseDTO
    {
        public bool IsActive { get; set; } = true;
    }

    public class CategoriaUpdateDTO : BaseDTO
    { }
}