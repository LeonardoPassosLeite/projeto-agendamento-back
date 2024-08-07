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
                .HasForeignKey(p => p.CategoriaId);

            builder.HasOne(p => p.FotoPrincipal)
                .WithOne(f => f.Produto)
                .HasForeignKey<Foto>(f => f.ProdutoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Funcionario)
                .WithMany(f => f.Produtos)
                .HasForeignKey(p => p.FuncionarioId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasData(
                new Produto
                {
                    Id = 1,
                    Nome = "Produto Inicial",
                    Descricao = "Descrição do Produto Inicial",
                    Preco = 100.00M,
                    CategoriaId = 1,
                    IsActive = true,
                    IsRascunho = true,
                    DataCriacao = DateTime.UtcNow,
                    DataAtualizacao = DateTime.UtcNow
                }
            );
        }
    }
}
