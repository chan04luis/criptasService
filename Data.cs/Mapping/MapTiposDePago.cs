using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Mapping
{
    public partial class MapTiposDePago : IEntityTypeConfiguration<TiposDePago>
    {
        public void Configure(EntityTypeBuilder<TiposDePago> builder)
        {
            builder.ToTable("TiposDePago");

            builder.HasKey(e => e.uId).HasName("Id");

            builder.Property(e => e.sNombre)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("nombre");

            builder.Property(e => e.sDescripcion)
                .HasColumnType("VARCHAR")
                .HasColumnName("descripcion");

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                     v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                     v =>v
                     );

            builder.Property(e => e.bEstatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");
        }
    }
}
