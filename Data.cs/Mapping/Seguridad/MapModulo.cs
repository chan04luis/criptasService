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
        private readonly string Esquema;

        public MapModulo(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Modulo> builder)
        {
            builder.ToTable("MODULOS", Esquema);

            builder.HasKey(e => e.uIdModulo)
                .HasName("MODULOS_PKEY");

            builder.Property(e => e.uIdModulo)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(e => e.sClaveModulo)
                .HasColumnName("CLAVE_MODULO")
                .HasColumnType("VARCHAR2(50)")
                .IsRequired();

            builder.Property(e => e.sNombreModulo)
                .HasColumnName("NOMBRE_MODULO")
                .HasColumnType("VARCHAR2(100)")
                .IsRequired();

            builder.Property(e => e.sPathModulo)
                .HasColumnName("PATH_MODULO")
                .HasColumnType("VARCHAR2(250)");

            builder.Property(e => e.bMostrarEnMenu)
                .HasColumnName("MOSTRAR_EN_MENU")
                .HasColumnType("NUMBER(1)");

            builder.Property(e => e.bActivo)
                .HasColumnName("ACTIVO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

        }
    }
}