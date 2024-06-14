using Agendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.Infra.Data.EntitiesConfiguration
{
    public class ClienteConfiguracao : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(c => c.Nome).HasMaxLength(100).IsRequired();
            builder.Property(c=>c.Telefone).HasMaxLength(9);
            builder.Property(c=>c.Cpf).HasMaxLength(11);
            builder.Property(c=>c.Cep).HasMaxLength(8);
            builder.Property(c=>c.Foto).HasMaxLength(250);
        }
    }
}