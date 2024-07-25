namespace Agendamento.Application.Helpers
{
    public class PaginationParams
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string? Filter { get; set; }
    }
}