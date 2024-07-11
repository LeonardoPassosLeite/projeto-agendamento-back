using Agendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.Infra.Data.EntitiesConfiguration
{
    public class FotoConfiguracao : IEntityTypeConfiguration<Foto>
    {
        public void Configure(EntityTypeBuilder<Foto> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd();

            builder.Property(f => f.Url)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.Property(f => f.FilePath)
                .HasMaxLength(255)
                .IsRequired(false);

            builder.HasIndex(f => f.ProdutoId);
        }
    }
}