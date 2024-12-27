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
    public partial class MapModulo : IEntityTypeConfiguration<Modulo>
    {
        public void Configure(EntityTypeBuilder<Modulo> builder)
        {
            // table
            builder.ToTable("Modulos");

            // key
            builder.HasKey(e => e.uIdModulo).HasName("PK_Modulos");

            // properties

            builder.Property(e => e.uIdModulo)
                .HasColumnType("uuid")
                .HasColumnName("Id");

            builder.Property(e => e.sClaveModulo)
                .HasColumnType("VARCHAR(50)")
                .IsUnicode(false)
                .HasColumnName("ClaveModulo");

            builder.Property(e => e.sNombreModulo)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("NombreModulo");

            builder.Property(e => e.sPathModulo)
                .HasColumnType("VARCHAR(250)")
                .IsUnicode(false)
                .HasColumnName("PathModulo");

            builder.Property(e => e.bMostrarEnMenu)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("MostrarEnMenu");

            builder.Property(e => e.bActivo)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("Activo");
        }
    }
}
