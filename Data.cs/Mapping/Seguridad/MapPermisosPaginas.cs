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
            builder.ToTable("PermisosPaginas");

            builder.HasKey(e => e.uIdPermisoPagina).HasName("PK_PermisosPagina");

            builder.Property(e => e.uIdPermisoPagina)
                .HasColumnType("uuid")
                .HasColumnName("IdPermisoPagina");

            builder.Property(e => e.uIdPerfil)
                .HasColumnType("uuid")
                .HasColumnName("IdPerfil");

            builder.Property(e => e.uIdPagina)
                .HasColumnType("uuid")
                .IsUnicode(false)
                .HasColumnName("IdPagina");

            builder.Property(e => e.bTienePermiso)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("TienePermiso");

            builder.Property(e => e.dtFechaCreacion)
                .HasColumnType("datetime")
                .IsUnicode(false)
                .HasColumnName("FechaCreacion");

            builder.Property(e => e.dtFechaModificacion)
               .HasColumnType("datetime")
               .IsUnicode(false)
               .HasColumnName("FechaModificacion");

            builder.Property(e => e.uIdUsuarioModificacion)
                .HasColumnType("uuid")
                .HasColumnName("IdUsuarioModificacion");

            builder.Property(e => e.bActivo)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("Activo");
        }
    }
}
