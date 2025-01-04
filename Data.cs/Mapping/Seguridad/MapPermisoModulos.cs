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
    public partial class MapPermisoModulos : IEntityTypeConfiguration<PermisoModulos>
    {
        public void Configure(EntityTypeBuilder<PermisoModulos> builder)
        {
            builder.ToTable("permisos_modulos");

            builder.HasKey(e => e.uIdPermisoModulo).HasName("PK_PermisosModulos");

            builder.Property(e => e.uIdPermisoModulo)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.uIdPerfil)
                .HasColumnType("uuid")
                .HasColumnName("id_perfil");

            builder.Property(e => e.uIdModulo)
                .HasColumnType("uuid")
                .IsUnicode(false)
                .HasColumnName("id_modulo");

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

            builder.Property(e => e.bActivo)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("activo");
        }
    }
}
