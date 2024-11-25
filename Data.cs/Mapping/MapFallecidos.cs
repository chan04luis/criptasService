using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities;

namespace Data.cs.Mapping
{
    public partial class MapFallecidos : IEntityTypeConfiguration<Fallecidos>
    {
        public void Configure(EntityTypeBuilder<Fallecidos> builder)
        {
            builder.ToTable("fallecidos");

            builder.HasKey(e => e.uId).HasName("id");

            builder.Property(e => e.uIdCripta)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.sNombre)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("nombre");

            builder.Property(e => e.dtFechaFallecimiento)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_fallecimiento");

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.dtFechaActializacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_eliminado")
                .HasConversion(v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                v => v );

            builder.Property(e => e.bEstatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");

            builder.Property(e => e.bEliminado)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("eliminado");

        }
    }
}
