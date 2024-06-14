using Agendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.Infra.Data.EntitiesConfiguration
{
    public class ProdutoConfiguracao : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Descricao).HasMaxLength(200).IsRequired();
            builder.Property(p => p.Preco).HasPrecision(10, 2);

            builder.HasOne(e => e.Categoria).WithMany(e => e.Produtos)
                .HasForeignKey(e => e.CategoriaId);

            builder.HasOne(e => e.Cliente).WithMany(e => e.Produtos)
                .HasForeignKey(e => e.ClienteId);
        }
    }
}