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

            builder.HasKey(e => e.id).HasName("id");

            builder.Property(e => e.id_clientes)
                .HasColumnType("uuid")
                .HasColumnName("id_clientes");

            builder.Property(e => e.id_cripta)
                .HasColumnType("uuid")
                .HasColumnName("id_cripta");

            builder.Property(e => e.id_tipo_pago)
                .HasColumnType("uuid")
                .HasColumnName("id_tipo_pago");

            builder.Property(e => e.monto_total)
                .HasColumnType("NUMERIC")
                .HasColumnName("monto_total");

            builder.Property(e => e.fecha_limite)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_limite");

            builder.Property(e => e.pagado)
                .HasColumnType("boolean")
                .HasColumnName("pagado");

            builder.Property(e => e.fecha_registro)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_registro");

            builder.Property(e => e.fecha_actualizacion)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_actualizacion");

            builder.Property(e => e.estatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");
        }
    }
}
