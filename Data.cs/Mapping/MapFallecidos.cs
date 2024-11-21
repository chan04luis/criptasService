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
    public partial class MapFallecidos : IEntityTypeConfiguration<Fallecidos>
    {
        public void Configure(EntityTypeBuilder<Fallecidos> builder)
        {
            builder.ToTable("Fallecidos");

            builder.HasKey(e => e.id).HasName("id");

            builder.Property(e => e.id_cirpta)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.nombre)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("nombre");

            builder.Property(e => e.fecha_fallecimiento)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_fallecimiento");

            builder.Property(e => e.fecha_registro)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_registro");

            builder.Property(e => e.estatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");

        }
    }
}
