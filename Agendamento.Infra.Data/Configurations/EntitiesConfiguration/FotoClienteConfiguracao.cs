using Agendamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Agendamento.Infra.Data.EntitiesConfiguration
{
    public class FotoClienteConfiguracao : IEntityTypeConfiguration<FotoCliente>
    {
        public void Configure(EntityTypeBuilder<FotoCliente> builder)
        {
            builder.HasKey(fc => fc.Id);
            builder.Property(fc => fc.Id).ValueGeneratedOnAdd();
            builder.Property(fc => fc.Url).HasMaxLength(255).IsRequired();
            builder.Property(fc => fc.IsPrincipal).IsRequired();

            builder.HasOne(fc => fc.Cliente)
                 .WithOne(p => p.FotoPrincipal)
                 .HasForeignKey<FotoCliente>(fc => fc.ClienteId);
        }
    }
}