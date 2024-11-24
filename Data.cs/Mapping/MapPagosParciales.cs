using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.cs.Entities;

namespace Data.cs.Mapping
{
    public partial class MapPagosParciales : IEntityTypeConfiguration<PagosParciales>
    {
        public void Configure(EntityTypeBuilder<PagosParciales> builder)
        {
            builder.ToTable("PagosParciales");

            builder.HasKey(e => e.uId).HasName("Id");

            builder.Property(e => e.uIdPago)
                .HasColumnType("uuid")
                .HasColumnName("Id_PAGOS");

            builder.Property(e => e.dMonto)
                .HasColumnType("NUMERIC")
                .HasColumnName("monto");

            builder.Property(e => e.dtFechaPago)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_pago");

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
