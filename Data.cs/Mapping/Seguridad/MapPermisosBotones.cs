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
    public partial class MapPermisosBotones : IEntityTypeConfiguration<PermisoBotones>
    {
        private readonly string Esquema;

        public MapPermisosBotones(string Esquema)
        {
            this.Esquema = Esquema;
        }
        public void Configure(EntityTypeBuilder<PermisoBotones> builder)
        {
            // table
            builder.ToTable("permisos_botones", Esquema);

            // key
            builder.HasKey(e => e.uIdPermisoBoton).HasName("PK_PermisosBotones");

            // properties
            builder.Property(e => e.uIdPermisoBoton)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.uIdPerfil)
                .HasColumnType("uuid")
                .HasColumnName("id_perfil");

            builder.Property(e => e.uIdBoton)
                .HasColumnType("uuid")
                .IsUnicode(false)
                .HasColumnName("id_boton");

            builder.Property(e => e.bTienePermiso)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("tiene_permiso");

            builder.Property(e => e.dtFechaCreacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.dtFechaModificacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_modificacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.uIdUsuarioModificacion)
                .HasColumnType("uuid")
                .HasColumnName("usuario_modificacion");

            builder.Property(e => e.bActivo)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("activo");

            // relaciones
            builder
               .HasOne(c => c.boton)
               .WithMany(e => e.lstPermisosBotones)
               .HasForeignKey(c => c.uIdBoton)
               .HasConstraintName("fk_boton");

            builder
                 .HasOne(pm => pm.perfil)
                 .WithMany()
                 .HasForeignKey(pm => pm.uIdPerfil);
        }
    }
}
