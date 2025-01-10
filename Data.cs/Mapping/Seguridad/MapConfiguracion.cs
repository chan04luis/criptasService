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

        public MapConfiguracion(string Esquema)
        {
            this.Esquema = Esquema;
        }
        public void Configure(EntityTypeBuilder<Configuracion> builder)
        {
            // table
            builder.ToTable("configuracion", Esquema);

            // key
            builder.HasKey(e => e.uIdConfiguracion).HasName("PK_Configuracion");

            // properties
            builder.Property(e => e.uIdConfiguracion)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.sTituloNavegador)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("titulo_navegador");

            builder.Property(e => e.sMetaDescripcion)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("meta_descripcion");

            builder.Property(e => e.sColorPrimario)
                .HasColumnType("VARCHAR(7)")
                .IsUnicode(false)
                .HasColumnName("color_primario");

            builder.Property(e => e.sColorSecundario)
                .HasColumnType("VARCHAR(7)")
                .IsUnicode(false)
                .HasColumnName("color_secundario");

            builder.Property(e => e.sContrastePrimario)
                .HasColumnType("VARCHAR(7)")
                .IsUnicode(false)
                .HasColumnName("contraste_primario");

            builder.Property(e => e.sContrasteSecundario)
                .HasColumnType("VARCHAR(7)")
                .IsUnicode(false)
                .HasColumnName("contraste_secundario");

            builder.Property(e => e.sUrlFuente)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("url_fuente");

            builder.Property(e => e.sNombreFuente)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("nombre_fuente");

            builder.Property(e => e.sRutaImagenFondo)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("ruta_imagen_fondo");

            builder.Property(e => e.sRutaImagenLogo)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("ruta_imagen_logo");

            builder.Property(e => e.sRutaImagenPortal)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("ruta_imagen_portal");

            builder.Property(e => e.dtFechaCreacion)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_creacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.uIdUsuarioCreacion)
              .HasColumnType("datetime")
              .IsUnicode(false)
              .HasColumnName("usuario_creacion");

            builder.Property(e => e.dtFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_modificacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.uIdUsuarioModificacion)
                .HasColumnType("uniqueidentifier")
                .HasColumnName("usuario_modificacion");

            builder.Property(e => e.bActivo)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("activo");

        }
    }
}
