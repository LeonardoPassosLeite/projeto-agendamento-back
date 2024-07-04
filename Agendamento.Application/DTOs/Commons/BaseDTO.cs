using System.Text.Json.Serialization;

namespace Agendamento.Application.DTOs.Commons
{
    public class BaseDTO
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
    }
}