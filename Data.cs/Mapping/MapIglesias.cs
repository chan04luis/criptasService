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
    public partial class MapIglesias : IEntityTypeConfiguration<Iglesias>
    {
        public void Configure(EntityTypeBuilder<Iglesias> builder)
        {
            builder.ToTable("Iglesias");

            builder.HasKey(e => e.id).HasName("id");

            builder.Property(e => e.nombre)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("nombre");

            builder.Property(e => e.direccion)
                .HasColumnType("VARCHAR")
                .HasColumnName("direccion");

            builder.Property(e => e.fecha_registro)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_registro");

            builder.Property(e => e.fecha_actualizacion)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_actualizacion");

            builder.Property(e => e.estatus)
                .HasColumnType("bollean")
                .HasColumnName("estatus");
        }
    }
}
