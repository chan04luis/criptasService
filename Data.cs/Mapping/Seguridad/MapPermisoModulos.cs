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
        private readonly string Esquema;

        public MapPermisoModulos(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<PermisoModulos> builder)
        {
            builder.ToTable("PERMISOS_MODULOS", Esquema);

            builder.HasKey(e => e.uIdPermisoModulo)
                   .HasName("PERMISOS_MODULOS_PKEY");

            builder.Property(e => e.uIdPermisoModulo)
                   .HasColumnName("ID")
                   .HasColumnType("RAW(16)")
                   .IsRequired();

            builder.Property(e => e.uIdPerfil)
                   .HasColumnName("ID_PERFIL")
                   .HasColumnType("RAW(16)")
                   .IsRequired();

            builder.Property(e => e.uIdModulo)
                   .HasColumnName("ID_MODULO")
                   .HasColumnType("RAW(16)")
                   .IsRequired();

            builder.Property(e => e.bTienePermiso)
                   .HasColumnName("TIENE_PERMISO")
                   .HasColumnType("NUMBER(1)")
                   .IsRequired();

            builder.Property(e => e.dtFechaCreacion)
                   .HasColumnName("FECHA_CREACION")
                   .HasColumnType("DATE");

            builder.Property(e => e.dtFechaModificacion)
                   .HasColumnName("FECHA_MODIFICACION")
                   .HasColumnType("DATE");

            builder.Property(e => e.bActivo)
                   .HasColumnName("ACTIVO")
                   .HasColumnType("NUMBER(1)")
                   .IsRequired();

        }
    }
}
