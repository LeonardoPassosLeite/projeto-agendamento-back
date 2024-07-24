using Agendamento.Application.DTOs;
using FluentValidation;
using Microsoft.AspNetCore.Http;

public class FotoUploadDTOValidator : AbstractValidator<FotoUploadDTO>
{
    private const long MaxFileSize = 5 * 1024 * 1024;

    public FotoUploadDTOValidator()
    {
        RuleFor(foto => foto.File)
            .NotNull()
            .WithMessage("Você deve fornecer um arquivo.")
            .Must(BeAValidFileType)
            .WithMessage("Tipo de arquivo não suportado. Apenas arquivos JPG, JPEG e PNG são permitidos.")
            .Must(BeWithinFileSizeLimit)
            .WithMessage($"O tamanho do arquivo não pode exceder {MaxFileSize / (1024 * 1024)} MB.");

        RuleFor(foto => foto.Url)
            .NotEmpty()
            .When(foto => foto.File == null)
            .WithMessage("Você deve fornecer um arquivo ou uma URL.");
    }

    private bool BeAValidFileType(IFormFile? file)
    {
        if (file == null)
            return false;

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var fileExtension = Path.GetExtension(file.FileName).ToLower();
        return allowedExtensions.Contains(fileExtension);
    }

    private bool BeWithinFileSizeLimit(IFormFile? file)
    {
        if (file == null)
            return false;

        return file.Length <= MaxFileSize;
    }
}