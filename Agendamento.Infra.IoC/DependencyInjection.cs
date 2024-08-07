using Agendamento.Application.Interfaces;
using Agendamento.Application.Mappings;
using Agendamento.Application.Services;
using Agendamento.Application.Validators;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;
using Agendamento.Infra.Data.Repositories;
using Agendamento.Infrastructure.Repositories;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agendamento.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructures(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            // Repositories
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFotoRepository, FotoRepository>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // Use Cases
            services.AddScoped<AddFotoToProduto>();
            services.AddScoped<UpdateStatusProduto>();
            services.AddScoped<GetProdutoByCategoriaId>();
            services.AddScoped<UpdateProduto>();

            // Services
            services.AddScoped<ICategoriaService, CategoriaService>();
            services.AddScoped<IClienteService, ClienteService>();
            services.AddScoped<IProdutoService, ProdutoService>();
            services.AddScoped<IFotoService, FotoService>();

            // AutoMapper
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            // FluentValidation
            services.AddValidatorsFromAssemblyContaining<ProdutoDTOValidator>();

            return services;
        }
    }
}