using Agendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.Infra.Data.EntitiesConfiguration
{
    public class CategoriaConfiguracao : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
               .ValueGeneratedOnAdd();
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();

            builder.HasData(
                new Categoria { Id = 1, Nome = "Categoria 1" },
                new Categoria { Id = 2, Nome = "Categoria 2" },
                new Categoria { Id = 3, Nome = "Categoria 3" },
                new Categoria { Id = 4, Nome = "Categoria 4" },
                new Categoria { Id = 5, Nome = "Categoria 5" }
            );
        }
    }
}