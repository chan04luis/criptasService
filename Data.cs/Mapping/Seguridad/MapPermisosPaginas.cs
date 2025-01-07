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
    public partial class MapPermisosPaginas : IEntityTypeConfiguration<PermisosPagina>
    {
        public void Configure(EntityTypeBuilder<PermisosPagina> builder)
        {
            builder.ToTable("permisos_paginas");

            builder.HasKey(e => e.uIdPermisoPagina).HasName("PK_PermisosPagina");

            builder.Property(e => e.uIdPermisoPagina)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.uIdPerfil)
                .HasColumnType("uuid")
                .HasColumnName("id_perfil");

            builder.Property(e => e.uIdPagina)
                .HasColumnType("uuid")
                .IsUnicode(false)
                .HasColumnName("id_pagina");

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
                .HasOne(c => c.pagina)
                .WithMany(e => e.lstPermisosPaginas)
                .HasForeignKey(c => c.uIdPagina)
                .HasConstraintName("fk_pagina");

            builder
                 .HasOne(pm => pm.perfil)
                 .WithMany()
                 .HasForeignKey(pm => pm.uIdPerfil);
        }
    }
}
