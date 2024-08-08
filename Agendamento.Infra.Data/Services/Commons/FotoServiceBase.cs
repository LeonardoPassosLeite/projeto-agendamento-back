using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities.Commons;
using Agendamento.Domain.Interfaces.Commons;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;

namespace Agendamento.Application.Services.Commons
{
    public abstract class FotoServiceBase<TFotoEntity, TFotoDTO>
     where TFotoEntity : FotoBase
     where TFotoDTO : class
    {
        private readonly StorageClient _storageClient;
        private readonly string _bucketName;
        private readonly IFotoRepository<TFotoEntity> _fotoRepository;
        protected readonly IMapper _mapper;
        private readonly IValidator<TFotoDTO> _validator;

        protected FotoServiceBase(IConfiguration configuration,
                                  IFotoRepository<TFotoEntity> fotoRepository,
                                  IMapper mapper,
                                  IValidator<TFotoDTO> validator)
        {
            _storageClient = StorageClient.Create();
            _bucketName = configuration["Firebase:BucketName"] ?? throw new ArgumentNullException(nameof(configuration));
            _fotoRepository = fotoRepository ?? throw new ArgumentNullException(nameof(fotoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        protected async Task<TFotoDTO> UploadFileAsync(TFotoDTO fotoUploadDto, Func<TFotoEntity, Task> additionalValidation = null)
        {
            if (fotoUploadDto == null)
                throw new ValidationException("DTO não pode ser nulo.");

            ValidationResult validationResult = await _validator.ValidateAsync(fotoUploadDto);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            string url = null;
            string filePath = null;

            if (fotoUploadDto is IFotoUpload fotoUpload)
            {
                if (fotoUpload.File != null)
                {
                    var fileName = fotoUpload.File.FileName;
                    url = $"https://firebasestorage.googleapis.com/v0/b/{_bucketName}/o/{fileName}?alt=media";
                    filePath = fileName;

                    using (var stream = fotoUpload.File.OpenReadStream())
                    {
                        await _storageClient.UploadObjectAsync(_bucketName, fileName, null, stream);
                    }
                }
                else if (!string.IsNullOrEmpty(fotoUpload.Url))
                {
                    url = fotoUpload.Url;
                    filePath = url;
                }

                if (string.IsNullOrEmpty(url))
                {
                    throw new ValidationException("Você deve fornecer um arquivo ou uma URL válida.");
                }

                var fotoEntity = _mapper.Map<TFotoEntity>(fotoUploadDto);
                fotoEntity.SetFileProperties(url, filePath, true);
                fotoEntity.SetId(Math.Abs(Guid.NewGuid().GetHashCode()));

                if (additionalValidation != null)
                {
                    await additionalValidation(fotoEntity);
                }

                try
                {
                    await _fotoRepository.AddAsync(fotoEntity);
                    return _mapper.Map<TFotoDTO>(fotoEntity);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("Ocorreu um erro ao adicionar a entidade.", ex);
                }
            }

            throw new InvalidOperationException("DTO não implementa IFotoUpload.");
        }
    }
}