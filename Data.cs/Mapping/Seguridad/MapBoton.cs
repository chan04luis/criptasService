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
        private readonly string Esquema;

        public MapBoton(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Boton> builder)
        {
            builder.ToTable("BOTONES", Esquema);

            builder.HasKey(e => e.uIdBoton)
                .HasName("BOTONES_PKEY");

            builder.Property(e => e.uIdBoton)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(e => e.uIdPagina)
                .HasColumnName("ID_PAGINA")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(e => e.sClaveBoton)
                .HasColumnName("CLAVE_BOTON")
                .HasColumnType("VARCHAR2(50)")
                .IsRequired();

            builder.Property(e => e.sNombreBoton)
                .HasColumnName("NOMBRE_BOTON")
                .HasColumnType("VARCHAR2(100)")
                .IsRequired();

            builder.Property(e => e.bActivo)
                .HasColumnName("ACTIVO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

        }
    }
}