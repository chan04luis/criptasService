using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.cs.Entities.Seguridad;

namespace Data.cs.Mapping.Seguridad
{
    public partial class MapConfiguracion : IEntityTypeConfiguration<Configuracion>
    {
        public void Configure(EntityTypeBuilder<Configuracion> builder)
        {
            // table
            builder.ToTable("Configuracion");

            // key
            builder.HasKey(e => e.uIdConfiguracion).HasName("PK_Configuracion");

            // properties
            builder.Property(e => e.uIdConfiguracion)
                .HasColumnType("uuid")
                .HasColumnName("Id");

            builder.Property(e => e.sTituloNavegador)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("TituloNavegador");

            builder.Property(e => e.sMetaDescripcion)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("MetaDescription");

            builder.Property(e => e.sColorPrimario)
                .HasColumnType("VARCHAR(7)")
                .IsUnicode(false)
                .HasColumnName("ColorPrimario");

            builder.Property(e => e.sColorSecundario)
                .HasColumnType("VARCHAR(7)")
                .IsUnicode(false)
                .HasColumnName("ColorSecundario");

            builder.Property(e => e.sContrastePrimario)
                .HasColumnType("VARCHAR(7)")
                .IsUnicode(false)
                .HasColumnName("ContrastePrimario");

            builder.Property(e => e.sContrasteSecundario)
                .HasColumnType("VARCHAR(7)")
                .IsUnicode(false)
                .HasColumnName("ContrasteSecundario");

            builder.Property(e => e.sUrlFuente)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("UrlFuente");

            builder.Property(e => e.sNombreFuente)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("NombreFuente");

            builder.Property(e => e.sRutaImagenFondo)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("RutaImagenFondo");

            builder.Property(e => e.sRutaImagenLogo)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("RutaImagenLogo");

            builder.Property(e => e.sRutaImagenPortal)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("RutaImagenPortal");

            builder.Property(e => e.dtFechaCreacion)
                .HasColumnType("datetime")
                .IsUnicode(false)
                .HasColumnName("FechaCreacion");

            builder.Property(e => e.dtFechaModificacion)
               .HasColumnType("datetime")
               .IsUnicode(false)
               .HasColumnName("FechaModificacion");

            builder.Property(e => e.uIdUsuarioModificacion)
                .HasColumnType("uniqueidentifier")
                .HasColumnName("IdUsuarioModificacion");

            builder.Property(e => e.bActivo)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("bActivo");

        }
    }
}
