using Agendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.Infra.Data.EntitiesConfiguration
{
    public class FotoProdutoConfiguracao : IEntityTypeConfiguration<FotoProduto>
    {
        public void Configure(EntityTypeBuilder<FotoProduto> builder)
        {
            builder.HasKey(fp => fp.Id);
            builder.Property(fp => fp.Id).ValueGeneratedOnAdd();
            builder.Property(fp => fp.Url).HasMaxLength(255);
            builder.Property(fp => fp.FilePath).HasMaxLength(255);
            builder.Property(fp => fp.IsPrincipal).IsRequired();

            builder.HasOne(fp => fp.Produto)
                 .WithOne(p => p.FotoPrincipal)
                 .HasForeignKey<FotoProduto>(fp => fp.ProdutoId);
        }
    }
}
