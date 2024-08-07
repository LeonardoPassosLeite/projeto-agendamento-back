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
                 .WithOne(p => p.FotoPrincipal)
                 .HasForeignKey<Foto>(f => f.ProdutoId);
        }
    }
}
