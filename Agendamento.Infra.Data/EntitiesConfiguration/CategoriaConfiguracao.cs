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
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();

            builder.HasData(
                new Categoria(1, "Categoria 1"),
                new Categoria(2, "Categoria 2"),
                new Categoria(3, "Categoria 3"),
                new Categoria(4, "Categoria 4")
            );
        }
    }
}