using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Orbita_challenge_backend_domain.Entities;

namespace Orbita_challenge_backend_Infra.EntityConfigurations
{
    public class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
    {
        public void Configure(EntityTypeBuilder<Aluno> builder)
        {
            builder.ToTable("Aluno");

            builder.HasKey(x => x.RA);

            builder.Property(x => x.Nome)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.RA)
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.CPF)
                .HasColumnType("char")
                .HasMaxLength(14)
                .IsRequired();
        }
    }
}
