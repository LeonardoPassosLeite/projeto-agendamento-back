using Agendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.Infra.Data.EntitiesConfiguration
{
    public class ClienteEmpresaConfiguracao : IEntityTypeConfiguration<ClienteEmpresa>
    {
        public void Configure(EntityTypeBuilder<ClienteEmpresa> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(ce => ce.Nome).HasMaxLength(100).IsRequired();
            builder.Property(ce => ce.Telefone).HasMaxLength(9);
            builder.Property(ce => ce.Cpf).HasMaxLength(11);
            builder.Property(ce => ce.CargoCliente).HasMaxLength(50);
            builder.Property(ce => ce.Foto).HasMaxLength(250);
        }
    }
}