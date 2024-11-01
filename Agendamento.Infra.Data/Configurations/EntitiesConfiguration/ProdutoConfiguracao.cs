using Agendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.Infra.Data.EntitiesConfiguration
{
    public class ProdutoConfiguracao : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Nome).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Localidade).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Descricao).HasMaxLength(200);
            builder.Property(p => p.Marca).HasMaxLength(50).IsRequired();
            builder.Property(p => p.Preco).HasPrecision(10, 2);
            builder.Property(p => p.Ano).IsRequired();
            builder.Property(p => p.Quilometragem).IsRequired();

            builder.HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(p => p.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.FotoPrincipal)
                .WithOne(fp => fp.Produto)
                .HasForeignKey<FotoProduto>(fp => fp.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(
                new Produto
                {
                    Id = 1,
                    Nome = "Produto Inicial",
                    Descricao = "Descrição do Produto Inicial",
                    Preco = 100.00M,
                    CategoriaId = 1,
                    Marca = "Marca 1",
                    Localidade = "Local 1",
                    Ano = 1997,
                    Quilometragem = 2000
                }
            );
        }
    }
}