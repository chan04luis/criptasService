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
        public void Configure(EntityTypeBuilder<PermisoBotones> builder)
        {
            // table
            builder.ToTable("PermisosBotones");

            // key
            builder.HasKey(e => e.uIdPermisoBoton).HasName("PK_PermisosBotones");

            // properties
            builder.Property(e => e.uIdPermisoBoton)
                .HasColumnType("uuid")
                .HasColumnName("IdPermisoBoton");

            builder.Property(e => e.uIdPerfil)
                .HasColumnType("uuid")
                .HasColumnName("IdPerfil");

            builder.Property(e => e.uIdBoton)
                .HasColumnType("uuid")
                .IsUnicode(false)
                .HasColumnName("IdBoton");

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
