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
            builder.ToTable("PermisosModulos");

            builder.HasKey(e => e.uIdPermisoModulo).HasName("PK_PermisosModulos");

            builder.Property(e => e.uIdPermisoModulo)
                .HasColumnType("uuid")
                .HasColumnName("IdPermisoModulo");

            builder.Property(e => e.uIdPerfil)
                .HasColumnType("uuid")
                .HasColumnName("IdPerfil");

            builder.Property(e => e.uIdModulo)
                .HasColumnType("uuid")
                .IsUnicode(false)
                .HasColumnName("IdModulo");

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
