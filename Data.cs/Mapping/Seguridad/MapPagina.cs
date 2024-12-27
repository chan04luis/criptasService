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
        public void Configure(EntityTypeBuilder<Pagina> builder)
        {
            // table
            builder.ToTable("Paginas");

            // key
            builder.HasKey(e => e.uIdPagina).HasName("PK_Paginas");

            // properties
            builder.Property(e => e.uIdPagina)
                .HasColumnType("uuid")
                .HasColumnName("Id");

            builder.Property(e => e.uIdModulo)
                .HasColumnType("uuid")
                .IsUnicode(false)
                .HasColumnName("IdModulo");

            builder.Property(e => e.sClavePagina)
                .HasColumnType("VARCHAR(50)")
                .IsUnicode(false)
                .HasColumnName("ClavePagina");

            builder.Property(e => e.sNombrePagina)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("NombrePagina");

            builder.Property(e => e.sPathPagina)
                .HasColumnType("VARCHAR(250)")
                .IsUnicode(false)
                .HasColumnName("PathPagina");

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
