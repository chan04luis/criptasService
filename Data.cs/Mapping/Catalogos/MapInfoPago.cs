using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.cs.Mapping.Catalogos
{
    public class MapInfoPago : IEntityTypeConfiguration<SolicitudPago>
    {
        private readonly string Esquema;

        public MapInfoPago(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<SolicitudPago> builder)
        {
            builder.ToTable("SOLICITUD_PAGO", Esquema);

            builder.HasKey(z => z.uId)
                .HasName("SOLICITUD_PAGO_PK");

            builder.Property(z => z.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(e => e.uIdPago)
                .HasColumnName("ID_PAGO")
                .HasColumnType("RAW(16)");

            builder.Property(e => e.sEvidencia)
                .HasColumnName("EVIDENCIA")
                .HasColumnType("CLOB");

            builder.Property(e => e.bEstatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)");

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP");

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");
        }
    }
}
