using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SB.Core.ValueObjects;

namespace SB.Credito.Infra.Mappings
{
    public class CreditoMapping : IEntityTypeConfiguration<Domain.Entities.Credito>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Credito> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UsuarioCod)
                .IsRequired()
                .HasColumnType("INT");

            builder.Property(x => x.UsuarioNome)
                .IsRequired()
                .HasColumnType("VARCHAR(1000)");

            builder.Property(x => x.DataNascimento)
               .IsRequired()
               .HasColumnType("DATETIME");

            builder.Property(x => x.Salario)
               .IsRequired()
               .HasColumnType("DECIMAL");

            builder.Property(x => x.Observacao)
               .IsRequired()
               .HasColumnType("VARCHAR(2000)");

            builder.OwnsOne(x => x.Cpf, tf =>
            {
                tf.Property(x => x.Number)
                    .IsRequired()
                    .HasMaxLength(CPF.CpfMaxLength)
                    .HasColumnName("Cpf")
                    .HasColumnType($"VARCHAR(${CPF.CpfMaxLength})");
            });

            builder.ToTable("Credito");
        }
    }
}
