using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Entities
{
    public partial class MapPagos : IEntityTypeConfiguration<Pagos>
    {
        public void Configure(EntityTypeBuilder<Pagos> builder)
        {
            builder.ToTable("Pagos");

            builder.HasKey(e => e.uId).HasName("id");

            builder.Property(e => e.uIdClientes)
                .HasColumnType("uuid")
                .HasColumnName("id_clientes");

            builder.Property(e => e.uIdCripta)
                .HasColumnType("uuid")
                .HasColumnName("id_cripta");

            builder.Property(e => e.uIdTipoPago)
                .HasColumnType("uuid")
                .HasColumnName("id_tipo_pago");

            builder.Property(e => e.montoTotal)
                .HasColumnType("NUMERIC")
                .HasColumnName("monto_total");

            builder.Property(e => e.dtFechaLimite)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_limite");

            builder.Property(e => e.bPagado)
                .HasColumnType("boolean")
                .HasColumnName("pagado");

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_registro");

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_actualizacion");

            builder.Property(e => e.bEstatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");
        }
    }
}
