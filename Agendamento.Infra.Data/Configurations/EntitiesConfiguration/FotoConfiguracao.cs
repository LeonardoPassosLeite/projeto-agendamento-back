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
            builder.Property(f => f.Url).HasMaxLength(255).IsRequired();
            builder.Property(f => f.FilePath).HasMaxLength(255).IsRequired();
            builder.Property(f => f.IsPrincipal).IsRequired();
            builder.Property(f => f.DataCriacao).IsRequired();
            builder.Property(f => f.DataAtualizacao).IsRequired();

            builder.HasOne(f => f.Produto)
                .WithMany()
                .HasForeignKey(f => f.ProdutoId);

            builder.HasData(
                new
                {
                    Id = 2019664857,
                    Url = "https://exemplo.com/foto1.jpg",
                    FilePath = "https://exemplo.com/foto1.jpg",
                    IsPrincipal = true,
                    ProdutoId = 1,
                    DataCriacao = DateTime.UtcNow,
                    DataAtualizacao = DateTime.UtcNow
                }
            );
        }
    }
}
