using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.cs.Mapping.Catalogos
{
    public class MapPagos : IEntityTypeConfiguration<Pagos>
    {
        private readonly string Esquema;

        public MapPagos(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Pagos> builder)
        {
            builder.ToTable("PAGOS", Esquema);

            builder.HasKey(p => p.uId)
                .HasName("PAGOS_PKEY");

            builder.Property(p => p.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(p => p.uIdClientes)
                .HasColumnName("ID_CLIENTE")
                .HasColumnType("RAW(16)");

            builder.Property(p => p.uIdCripta)
                .HasColumnName("ID_CRIPTA")
                .HasColumnType("RAW(16)");

            builder.Property(p => p.uIdTipoPago)
                .HasColumnName("ID_TIPO_PAGO")
                .HasColumnType("RAW(16)");

            builder.Property(p => p.iTipoPago)
                .HasColumnName("TIPO_PAGOS")
                .HasColumnType("NUMBER")
                .IsRequired();

            builder.Property(p => p.dMontoTotal)
                .HasColumnName("MONTO_TOTAL")
                .HasColumnType("NUMBER(10,2)")
                .IsRequired();

            builder.Property(p => p.dMontoPagado)
                .HasColumnName("MONTO_PAGADO")
                .HasColumnType("NUMBER(10,2)");

            builder.Property(p => p.dtFechaLimite)
                .HasColumnName("FECHA_LIMITE")
                .HasColumnType("DATE")
                .IsRequired();

            builder.Property(p => p.dtFechaPago)
                .HasColumnName("FECHA_PAGADO")
                .HasColumnType("DATE");

            builder.Property(p => p.bPagado)
                .HasColumnName("PAGADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(p => p.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(p => p.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(p => p.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(p => p.bEstatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)");

            builder.Property(p => p.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            // Relaciones
            builder.HasOne(p => p.Cliente)
                .WithMany(c => c.listPagos)
                .HasForeignKey(p => p.uIdClientes)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Cripta)
                .WithMany(c => c.listPagos)
                .HasForeignKey(p => p.uIdCripta)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.TipoPago)
                .WithMany()
                .HasForeignKey(p => p.uIdTipoPago)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
