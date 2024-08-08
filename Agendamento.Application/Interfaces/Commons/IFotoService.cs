namespace Agendamento.Application.Interfaces
{
    public interface IFotoService<TDto>
    {
        Task<TDto> UploadFileAsync(TDto fotoDto);
    }
}