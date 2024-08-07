using Agendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.Infra.Data.EntitiesConfiguration
{
    public class ClienteConfiguracao : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Nome).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Cpf).HasMaxLength(11).IsRequired();
            builder.Property(c => c.Idade).IsRequired();
            builder.Property(c => c.Telefone).HasMaxLength(15).IsRequired();
            builder.Property(c => c.Cep).HasMaxLength(8).IsRequired();
            builder.Property(c => c.Endereco).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Cidade).HasMaxLength(100).IsRequired();
            builder.Property(c => c.Uf).HasMaxLength(2).IsRequired();
            builder.Property(c => c.Pais).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Empresa).HasMaxLength(50).IsRequired();
            builder.Property(c => c.IsVisit).IsRequired();

            builder.HasOne(c => c.FotoPrincipal)
                .WithOne()
                .HasForeignKey<Cliente>(c => c.FotoPrincipalId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
           new Cliente
           {
               Id = 1,
               Nome = "Cliente Inicial",
               Cpf = "12345678901",
               Idade = 30,
               Telefone = "1234567890",
               Cep = "12345678",
               Endereco = "Rua Exemplo, 123",
               Cidade = "Cidade Exemplo",
               Uf = "EX",
               Pais = "País Exemplo",
               Empresa = "Empresa Inical",
               IsVisit = true,
               FotoPrincipalId = null
           });
        }
    }
}