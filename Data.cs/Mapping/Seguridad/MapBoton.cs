using Data.cs.Entities.Seguridad;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Mapping.Seguridad
{
    public partial class MapBoton : IEntityTypeConfiguration<Boton>
    {
        public void Configure(EntityTypeBuilder<Boton> builder)
        {

            builder.ToTable("Botones");

            builder.HasKey(e => e.uIdBoton).HasName("PK_Botones");

            builder.Property(e => e.uIdBoton)
                .HasColumnType("uuid")
                .HasColumnName("Id");

            builder.Property(e => e.uIdPagina)
                .HasColumnType("uuid")
                .IsUnicode(false)
                .HasColumnName("iIdPagina");

            builder.Property(e => e.sClaveBoton)
                .HasColumnType("VARCHAR(50)")
                .IsUnicode(false)
                .HasColumnName("sClaveBoton");

            builder.Property(e => e.sNombreBoton)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("sNombreBoton");

            builder.Property(e => e.bActivo)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("Activo");

        }
    }
}
