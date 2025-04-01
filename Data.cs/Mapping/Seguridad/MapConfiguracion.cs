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
        private readonly string Esquema;

        public MapConfiguracion(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Configuracion> builder)
        {
            builder.ToTable("CONFIGURACION", Esquema);

            builder.HasKey(e => e.uIdConfiguracion)
                .HasName("CONFIGURACION_PKEY");

            builder.Property(e => e.uIdConfiguracion)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(e => e.sTituloNavegador)
                .HasColumnName("TITULO_NAVEGADOR")
                .HasColumnType("VARCHAR2(500)");

            builder.Property(e => e.sMetaDescripcion)
                .HasColumnName("META_DESCRIPCION")
                .HasColumnType("VARCHAR2(500)");

            builder.Property(e => e.sColorPrimario)
                .HasColumnName("COLOR_PRIMARIO")
                .HasColumnType("VARCHAR2(7)");

            builder.Property(e => e.sColorSecundario)
                .HasColumnName("COLOR_SECUNDARIO")
                .HasColumnType("VARCHAR2(7)");

            builder.Property(e => e.sContrastePrimario)
                .HasColumnName("CONTRASTE_PRIMARIO")
                .HasColumnType("VARCHAR2(7)");

            builder.Property(e => e.sContrasteSecundario)
                .HasColumnName("CONTRASTE_SECUNDARIO")
                .HasColumnType("VARCHAR2(7)");

            builder.Property(e => e.sUrlFuente)
                .HasColumnName("URL_FUENTE")
                .HasColumnType("VARCHAR2(500)");

            builder.Property(e => e.sNombreFuente)
                .HasColumnName("NOMBRE_FUENTE")
                .HasColumnType("VARCHAR2(500)");

            builder.Property(e => e.sRutaImagenFondo)
                .HasColumnName("RUTA_IMAGEN_FONDO")
                .HasColumnType("VARCHAR2(500)");

            builder.Property(e => e.sRutaImagenLogo)
                .HasColumnName("RUTA_IMAGEN_LOGO")
                .HasColumnType("VARCHAR2(500)");

            builder.Property(e => e.sRutaImagenPortal)
                .HasColumnName("RUTA_IMAGEN_PORTAL")
                .HasColumnType("VARCHAR2(500)");

            builder.Property(e => e.dtFechaCreacion)
                .HasColumnName("FECHA_CREACION")
                .HasColumnType("DATE");

            builder.Property(e => e.uIdUsuarioCreacion)
                .HasColumnName("USUARIO_CREACION")
                .HasColumnType("RAW(16)");

            builder.Property(e => e.dtFechaModificacion)
                .HasColumnName("FECHA_MODIFICACION")
                .HasColumnType("DATE");

            builder.Property(e => e.uIdUsuarioModificacion)
                .HasColumnName("USUARIO_MODIFICACION")
                .HasColumnType("RAW(16)");

            builder.Property(e => e.bActivo)
                .HasColumnName("ACTIVO")
                .HasColumnType("NUMBER(1)");
        }
    }
}