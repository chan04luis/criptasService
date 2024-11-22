using Data.cs.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Mapping
{
    public partial class MapIglesias : IEntityTypeConfiguration<Iglesias>
    {
        public void Configure(EntityTypeBuilder<Iglesias> builder)
        {
            builder.ToTable("iglesias");
            builder.HasKey(i => i.uId).HasName("PK_Iglesia");

            builder.Property(i => i.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(i => i.sNombre)
                .HasColumnType("VARCHAR(255)")
                .IsUnicode(false)
                .HasColumnName("nombre");

            builder.Property(i => i.sDireccion)
                .HasColumnType("TEXT")
                .HasColumnName("direccion");

            builder.Property(i => i.sCiudad)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("ciudad");

            builder.Property(i => i.dtFechaRegistro)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(i => i.dtFechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(i => i.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_eliminado")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(i => i.bEstatus)
                .HasColumnType("bolean")
                .IsUnicode(false)
                .HasColumnName("estatus");

            builder.Property(i => i.bEliminado)
                .HasColumnType("bolean")
                .IsUnicode(false)
                .HasColumnName("eliminado");
        }
    }
}
