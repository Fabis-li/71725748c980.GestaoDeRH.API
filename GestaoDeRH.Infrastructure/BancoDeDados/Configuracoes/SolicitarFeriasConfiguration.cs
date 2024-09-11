using GestaoDeRH.Dominio.Ferias;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GestaoDeRH.Infra.BancoDeDados.Configuracoes;

public class SolicitarFeriasConfiguration : IEntityTypeConfiguration<SolicitarFerias>
{
    public void Configure(EntityTypeBuilder<SolicitarFerias> builder)
    {
        builder.ToTable("Ferias");
        
        builder.HasKey(f => f.Id);

        builder.Property(f => f.DataInicioFerias)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(f => f.DataFimFerias)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.Property(f => f.Solicitacao)
            .IsRequired()
            .HasColumnType("datetime2");

        builder.HasOne(f => f.Colaborador)
            .WithMany()
            .HasForeignKey(x => x.ColaboradorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}