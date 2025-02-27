using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Mapping.Catalogos
{
    public class MapInfoPago : IEntityTypeConfiguration<SolicitudPago>
    {
        private readonly string Esquema;

        public MapInfoPago(string Esquema)
        {
            this.Esquema = Esquema;
        }
        public void Configure(EntityTypeBuilder<SolicitudPago> builder)
        {
            builder.ToTable("solicitud_pago", Esquema);

            builder.HasKey(z => z.uId)
                .HasName("solicitud_pago_pk");

            builder.Property(z => z.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.uIdPago)
                .HasColumnType("uuid")
                .HasColumnName("id_pago");

            builder.Property(e => e.sEvidencia)
                .HasColumnType("text")
                .HasColumnName("evidencia");

            builder.Property(e => e.bEstatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");

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
        }
    }
}
