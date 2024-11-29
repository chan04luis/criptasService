using Data.cs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.cs.Mapping
{
    public partial class MapBeneficiarios : IEntityTypeConfiguration<Beneficiarios>
    {
        public void Configure(EntityTypeBuilder<Beneficiarios> builder)
        {
            builder.ToTable("beneficiarios");
            builder.HasKey(z => z.uId)
                .HasName("PK_Beneficiarios");

            builder.Property(z => z.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.uIdCripta)
                .HasColumnType("uuid")
                .HasColumnName("id_cripta");

            builder.Property(e => e.sNombre)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("nombre");

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(c => c.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_eliminado")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.bEstatus)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("estatus");

            builder.Property(e => e.bEliminado)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("eliminado");
        }
    }
}
