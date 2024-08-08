namespace Agendamento.Domain.Interfaces
{
    public interface IFoto
    {
        int Id { get; set; }
        string? Url { get; set; }
        bool IsPrincipal { get; set; }
    }
}