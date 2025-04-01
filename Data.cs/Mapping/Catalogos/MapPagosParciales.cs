using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapPagosParciales : IEntityTypeConfiguration<PagosParciales>
    {
        private readonly string Esquema;

        public MapPagosParciales(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<PagosParciales> builder)
        {
            builder.ToTable("PAGOS_PARCIALES", Esquema);

            builder.HasKey(z => z.uId)
                .HasName("PAGOS_PARCIALES_PKEY");

            builder.Property(z => z.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(e => e.uIdPago)
                .HasColumnName("ID_PAGO")
                .HasColumnType("RAW(16)");

            builder.Property(e => e.dMonto)
                .HasColumnName("MONTO")
                .HasColumnType("NUMBER(10,2)")
                .IsRequired();

            builder.Property(e => e.dtFechaPago)
                .HasColumnName("FECHA_PAGO")
                .HasColumnType("DATE")
                .IsRequired();

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(e => e.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(e => e.bEstatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)");

            builder.Property(e => e.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

         
        }
    }
}
