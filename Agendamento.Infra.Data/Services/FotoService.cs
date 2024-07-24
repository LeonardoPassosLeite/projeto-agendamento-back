using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Exceptions;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;

public class FotoService : IFotoService
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName;
    private readonly IFotoRepository _fotoRepository;
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<FotoDTO> _validator;

    public FotoService(IConfiguration configuration, IFotoRepository fotoRepository, IProdutoRepository produtoRepository, IMapper mapper, IValidator<FotoDTO> validator)
    {
        _storageClient = StorageClient.Create();
        _bucketName = configuration["Firebase:BucketName"];
        _fotoRepository = fotoRepository;
        _produtoRepository = produtoRepository;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<FotoDTO> UploadFileAsync(FotoUploadDTO fotoUploadDto)
    {
        if (fotoUploadDto == null)
            throw new ValidationException("DTO não pode ser nulo.");

        string filePath;
        string url;

        if (fotoUploadDto.File != null)
        {
            var fileName = fotoUploadDto.File.FileName;
            filePath = $"https://firebasestorage.googleapis.com/v0/b/{_bucketName}/o/{fileName}?alt=media";

            using (var stream = fotoUploadDto.File.OpenReadStream())
            {
                await _storageClient.UploadObjectAsync(_bucketName, fileName, null, stream);
            }
        }
        else if (!string.IsNullOrEmpty(fotoUploadDto.Url))
            filePath = fotoUploadDto.Url;

        else
            throw new ValidationException("Você deve fornecer um arquivo ou uma URL.");


        url = filePath;

        var produto = await _produtoRepository.GetByIdAsync(fotoUploadDto.ProdutoId);
        if (produto == null)
            throw new NotFoundException($"Produto com Id {fotoUploadDto.ProdutoId} não encontrado.");

        if (!produto.IsRascunho)
            throw new ConflictException($"O produto com Id {fotoUploadDto.ProdutoId} ja possui uma foto principal.");

        try
        {
            var foto = _mapper.Map<Foto>(fotoUploadDto);
            foto.SetFileProperties(url, filePath, true);

            foto.SetId(Math.Abs(Guid.NewGuid().GetHashCode()));

            await _fotoRepository.AddAsync(foto);
            return _mapper.Map<FotoDTO>(foto);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Ocorreu um erro ao adicionar a entidade.", ex);
        }
    }
}