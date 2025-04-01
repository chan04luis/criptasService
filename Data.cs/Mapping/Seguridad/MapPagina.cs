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
    public partial class MapPagina : IEntityTypeConfiguration<Pagina>
    {
        private readonly string Esquema;

        public MapPagina(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Pagina> builder)
        {
            builder.ToTable("PAGINAS", Esquema);

            builder.HasKey(e => e.uIdPagina)
                .HasName("PAGINAS_PKEY");

            builder.Property(e => e.uIdPagina)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(e => e.uIdModulo)
                .HasColumnName("ID_MODULO")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(e => e.sClavePagina)
                .HasColumnName("CLAVE_PAGINA")
                .HasColumnType("VARCHAR2(50)")
                .IsRequired();

            builder.Property(e => e.sNombrePagina)
                .HasColumnName("NOMBRE_PAGINA")
                .HasColumnType("VARCHAR2(100)")
                .IsRequired();

            builder.Property(e => e.sPathPagina)
                .HasColumnName("PATH_PAGINA")
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
