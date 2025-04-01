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
        private readonly string Esquema;

        public MapPermisosPaginas(string Esquema)
        {
            this.Esquema = Esquema;
        }

        public void Configure(EntityTypeBuilder<PermisosPagina> builder)
        {
            builder.ToTable("PERMISOS_PAGINAS", Esquema);

            builder.HasKey(e => e.uIdPermisoPagina)
                   .HasName("PERMISOS_PAGINAS_PKEY");

            builder.Property(e => e.uIdPermisoPagina)
                   .HasColumnName("ID")
                   .HasColumnType("RAW(16)")
                   .IsRequired();

            builder.Property(e => e.uIdPerfil)
                   .HasColumnName("ID_PERFIL")
                   .HasColumnType("RAW(16)")
                   .IsRequired();

            builder.Property(e => e.uIdPagina)
                   .HasColumnName("ID_PAGINA")
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

            builder.Property(e => e.uIdUsuarioModificacion)
                   .HasColumnName("USUARIO_MODIFICACION")
                   .HasColumnType("RAW(16)");

            builder.Property(e => e.bActivo)
                   .HasColumnName("ACTIVO")
                   .HasColumnType("NUMBER(1)")
                   .IsRequired();
        }
    }
}