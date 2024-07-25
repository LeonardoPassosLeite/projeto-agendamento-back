using Agendamento.Domain.Interfaces;

public class PagedResultDTO<T> : IPagedResult<T>
{
    public IEnumerable<T> Items { get; set; }
    public int TotalCount { get; set; }

    public PagedResultDTO(IEnumerable<T> items, int totalCount)
    {
        Items = items;
        TotalCount = totalCount;
    }
}
