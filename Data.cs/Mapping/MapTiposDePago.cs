using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities;

namespace Data.cs.Mapping
{
    public partial class MapTiposDePago : IEntityTypeConfiguration<TiposDePago>
    {
        public void Configure(EntityTypeBuilder<TiposDePago> builder)
        {
            builder.ToTable("tipos_pago");

            builder.HasKey(z => z.uId)
                .HasName("PK_TiposPago");

            builder.Property(z => z.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.sNombre)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("nombre");

            builder.Property(e => e.sDescripcion)
                .HasColumnType("VARCHAR")
                .HasColumnName("descripcion");

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                     v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                     v =>v
                     );

            builder.Property(e => e.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_eliminado")
                .HasConversion(v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified), v => v);

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
