using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities;

namespace Data.cs.Mapping
{
    public partial class MapPagosParciales : IEntityTypeConfiguration<PagosParciales>
    {
        public void Configure(EntityTypeBuilder<PagosParciales> builder)
        {
            builder.ToTable("pagos_parciales");

            builder.HasKey(z => z.uId)
                .HasName("PK_PagosParciales");

            builder.Property(z => z.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.uIdPago)
                .HasColumnType("uuid")
                .HasColumnName("id_pago");

            builder.Property(e => e.dMonto)
                .HasColumnType("NUMERIC")
                .HasColumnName("monto");

            builder.Property(e => e.dtFechaPago)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_pago");

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
                    v => v
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
