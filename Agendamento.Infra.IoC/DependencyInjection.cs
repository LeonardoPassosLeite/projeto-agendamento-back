using Agendamento.Application.Interfaces;
using Agendamento.Application.Mappings;
using Agendamento.Application.Services;
using Agendamento.Domain.Interfaces;
using Agendamento.Infra.Data.Context;
using Agendamento.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Agendamento.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructures(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            services.AddScoped<ICategoriaService, CategoriaService>();
            // services.AddScoped<IProdutoService, ProdutoService>();
            services.AddAutoMapper(typeof(DoaminToDTOMappingProfile));

            return services;
        }
    }
}