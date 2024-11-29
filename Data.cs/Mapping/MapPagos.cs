using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Data.cs.Entities
{
    public partial class MapPagos : IEntityTypeConfiguration<Pagos>
    {
        public void Configure(EntityTypeBuilder<Pagos> builder)
        {
            builder.ToTable("pagos");

            builder.HasKey(z => z.uId)
                .HasName("PK_Pago");

            builder.Property(z => z.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.uIdClientes)
                .HasColumnType("uuid")
                .HasColumnName("id_cliente");

            builder.Property(e => e.uIdCripta)
                .HasColumnType("uuid")
                .HasColumnName("id_cripta");

            builder.Property(e => e.uIdTipoPago)
                .HasColumnType("uuid")
                .HasColumnName("id_tipo_pago");

            builder.Property(e => e.dMontoTotal)
                .HasColumnType("NUMERIC")
                .HasColumnName("monto_total");

            builder.Property(e => e.dtFechaLimite)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_limite");


            builder.Property(e => e.dMontoPagado)
                .HasColumnType("NUMERIC")
                .HasColumnName("monto_pagado");

            builder.Property(e => e.dtFechaPago)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_pagado");

            builder.Property(e => e.bPagado)
                .HasColumnType("boolean")
                .HasColumnName("pagado");

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
                .HasConversion(
                     v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                     v => v);

            builder.Property(e => e.bEstatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");

            builder.Property(e => e.bEliminado)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("eliminado");

            builder.HasOne(z => z.Cliente)
                .WithMany(i => i.listPagos)
                .HasForeignKey(z => z.uIdClientes)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(z => z.Cripta)
                .WithMany(i => i.listPagos)
                .HasForeignKey(z => z.uIdCripta)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(z => z.TipoPago)
                .WithMany(i => i.listPagos)
                .HasForeignKey(z => z.uIdTipoPago)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
