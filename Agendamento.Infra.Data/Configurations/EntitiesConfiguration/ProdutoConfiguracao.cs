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
            builder.Property(p => p.Nome).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Descricao).HasMaxLength(200);
            builder.Property(p => p.Preco).HasPrecision(10, 2);

            builder.HasOne(p => p.Categoria)
                .WithMany(c => c.Produtos)
                .HasForeignKey(c => c.CategoriaId);

            builder.HasOne(p => p.FotoPrincipal)
                .WithOne()
                .HasForeignKey<Produto>(p => p.FotoPrincipalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
                          new
                          {
                              Id = 1,
                              Nome = "Produto 1",
                              Preco = 100.00M,
                              Descricao = "Descrição Produto",
                              CategoriaId = 1,
                              FotoPrincipalId = (int?)null,
                              IsActive = true,
                              IsRascunho = true,
                              DataCriacao = DateTime.UtcNow,
                              DataAtualizacao = DateTime.UtcNow
                          },
                          new
                          {
                              Id = 2,
                              Nome = "Produto 2",
                              Preco = 200.00M,
                              Descricao = "Descrição Produto",
                              CategoriaId = 1,
                              FotoPrincipalId = (int?)null,
                              IsActive = true,
                              IsRascunho = true,
                              DataCriacao = DateTime.UtcNow,
                              DataAtualizacao = DateTime.UtcNow
                          },
                          new
                          {
                              Id = 3,
                              Nome = "Produto 3",
                              Preco = 300.00M,
                              Descricao = "Descrição Produto",
                              CategoriaId = 2,
                              FotoPrincipalId = (int?)null,
                              IsActive = true,
                              IsRascunho = true,
                              DataCriacao = DateTime.UtcNow,
                              DataAtualizacao = DateTime.UtcNow
                          }
                      );
        }
    }
}