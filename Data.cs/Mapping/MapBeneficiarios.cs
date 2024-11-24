using Data.cs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.cs.Mapping
{
    public partial class MapBeneficiarios : IEntityTypeConfiguration<Beneficiarios>
    {
        public void Configure(EntityTypeBuilder<Beneficiarios> builder)
        {
            builder.ToTable("beneficiaros");
            builder.HasKey(e => e.uId).HasName("id_Beneficiaros");

            builder.Property(e => e.uIdCripta)
                .HasColumnType("uuid")
                .HasColumnName("id_cripta");

            builder.Property(e => e.sNombre)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("nombre");

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnType("DateTime")
                .IsUnicode(false)
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_actuallizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.bEstatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");
        }
    }
}
