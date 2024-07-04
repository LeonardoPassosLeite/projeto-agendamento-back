using System.ComponentModel.DataAnnotations;
using Agendamento.Application.DTOs;
using Agendamento.Application.Interfaces;
using Agendamento.Domain.Entities;
using Agendamento.Domain.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Agendamento.Application.Services
{
    // public class ProdutoService : IProdutoService
    // {
    //     private readonly IProdutoRepository _produtoRepository;
    //     private readonly IMapper _mapper;

    //     public ProdutoService(IProdutoRepository produtoRepository, IMapper mapper) 
    //     {
    //         _produtoRepository = produtoRepository ?? throw new ArgumentNullException(nameof(produtoRepository));
    //         _mapper = mapper;
    //     }

    //     public async Task<ProdutoDTO> CreateProdutoAsync(ProdutoDTO produtoDto)
    //     {
    //         return await HandleExceptionAsync(async () =>
    //         {
    //             var produtoEntity = _mapper.Map<Produto>(produtoDto);

    //             if (produtoDto.FotoPrincipal != null)
    //             {
    //                 produtoEntity.FotoPrincipal = await SaveFileAsync(produtoDto.FotoPrincipal);
    //             }

    //             if (produtoDto.Fotos != null && produtoDto.Fotos.Count > 0)
    //             {
    //                 produtoEntity.Fotos = new List<string>();
    //                 foreach (var foto in produtoDto.Fotos)
    //                 {
    //                     var fotoPath = await SaveFileAsync(foto);
    //                     produtoEntity.Fotos.Add(fotoPath);
    //                 }
    //             }

    //             await _produtoRepository.CreateAsync(produtoEntity);
    //             return _mapper.Map<ProdutoDTO>(produtoEntity);
    //         }, "Ocorreu um erro ao adicionar um produto");
    //     }

    //     private async Task<string> SaveFileAsync(IFormFile file)
    //     {
    //         var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");

    //         if (!Directory.Exists(uploadsFolder))
    //         {
    //             Directory.CreateDirectory(uploadsFolder);
    //         }

    //         var filePath = Path.Combine(uploadsFolder, Guid.NewGuid().ToString() + Path.GetExtension(file.FileName));

    //         using (var stream = new FileStream(filePath, FileMode.Create))
    //         {
    //             await file.CopyToAsync(stream);
    //         }

    //         return filePath;
    //     }

    //     public async Task DisableProdutoAsync(ProdutoDTO produtoDto)
    //     {
    //         await HandleExceptionAsync(async () =>
    //        {
    //            var produtoEntity = await _produtoRepository.GetByIdAsync(produtoDto.Id);
    //            if (produtoEntity == null)
    //            {
    //                throw new KeyNotFoundException("Produto não encontrado");
    //            }

    //            produtoEntity.Disable();
    //            await _produtoRepository.UpdateAsync(produtoEntity);
    //            return Task.CompletedTask;
    //        }, $"Ocorreu um erro ao desativar o produto com id {produtoDto.Id}");
    //     }

    //     public async Task<IEnumerable<ProdutoDTO>> GetByCategoriaIdAsync(int? id)
    //     {
    //         if (id == null)
    //         {
    //             throw new ValidationException("Id da categoria não pode ser nulo.");
    //         }

    //         return await HandleExceptionAsync(async () =>
    //         {
    //             var produtos = await _produtoRepository.GetByCategoriaIdAsync(id.Value);
    //             return _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
    //         }, $"Ocorreu um erro ao obter os produtos da categoria com id {id}");
    //     }

    //     public async Task<ProdutoDTO> GetByIdAsync(int? id)
    //     {
    //         return await HandleExceptionAsync(async () =>
    //        {
    //            var produtoEntity = await _produtoRepository.GetByIdAsync(id);
    //            if (produtoEntity == null)
    //            {
    //                throw new KeyNotFoundException("Produto não encontrado");
    //            }
    //            return _mapper.Map<ProdutoDTO>(produtoEntity);
    //        }, $"Ocorreu um erro ao obter o produto com id {id}");
    //     }

    //     public async Task<IEnumerable<ProdutoDTO>> GetProdutosAsync()
    //     {
    //         return await HandleExceptionAsync(async () =>
    //       {
    //           var produtoEntities = await _produtoRepository.GetProdutosAsync();
    //           return _mapper.Map<IEnumerable<ProdutoDTO>>(produtoEntities);
    //       }, "Ocorreu um erro ao obter todos os produtos");
    //     }

    //     public async Task<ProdutoDTO> UpdateProdutoAsync(ProdutoDTO produtoDto)
    //     {
    //         return await HandleExceptionAsync(async () =>
    //         {
    //             var produtoEntity = await _produtoRepository.GetByIdAsync(produtoDto.Id);
    //             if (produtoEntity == null)
    //             {
    //                 throw new KeyNotFoundException("Produto não encontrado");
    //             }
    //             _mapper.Map(produtoDto, produtoEntity);
    //             await _produtoRepository.UpdateAsync(produtoEntity);
    //             return _mapper.Map<ProdutoDTO>(produtoEntity);
    //         }, $"Ocorreu um erro ao atualizar o produto com id {produtoDto.Id}");
    //     }
    // }
}