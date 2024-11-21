using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities;

namespace Data.cs.Mapping
{
    public partial class MapClientes : IEntityTypeConfiguration<Clientes>
    {
        public void Configure(EntityTypeBuilder<Clientes> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(c => c.id).HasName("PK_Cliente");

            builder.Property(c => c.id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(c => c.nombre)
                .HasColumnType("VARCHAR(255)")
                .IsUnicode(false)
                .HasColumnName("nombre");

            builder.Property(c => c.direccion)
                .HasColumnType("VARCHAR")
                .IsUnicode(false)
                .HasColumnName("direccion");

            builder.Property(c => c.telefono)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("telefono");

            builder.Property(c => c.email)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("email");

            builder.Property(c => c.fecha_registro)
                .HasColumnType("DateTime")
                .IsUnicode(false)
                .HasColumnName("fecha_registro");

            builder.Property(c => c.fecha_actualizacion)
                .HasColumnType("DateTime")
                .IsUnicode(false)
                .HasColumnName("fecha_actualizacion");

            builder.Property(c => c.estatus)
                .HasColumnType("bolean")
                .IsUnicode(false)
                .HasColumnName("estatus");
        }
    }
}
