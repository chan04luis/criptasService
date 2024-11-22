using Data.cs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Mapping
{
    public partial class MapBeneficiarios : IEntityTypeConfiguration<Beneficiarios>
    {
        public void Configure(EntityTypeBuilder<Beneficiarios> builder)
        {
            builder.ToTable("beneficiaros");
            builder.HasKey(e => e.id).HasName("id_Beneficiaros");

            builder.Property(e => e.id_cripta)
                .HasColumnType("uuid")
                .HasColumnName("id_cripta");

            builder.Property(e => e.nombre)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("nombre");

            builder.Property(e => e.fecha_registro)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_registro");

            builder.Property(e => e.fecha_actualizacion)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_actuallizacion");

            builder.Property(e => e.estatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");
        }
    }
}
