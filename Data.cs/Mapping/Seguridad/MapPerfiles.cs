using Data.cs.Entities;
using Data.cs.Entities.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Mapping.Seguridad
{
    public partial class MapPerfiles : IEntityTypeConfiguration<Perfil>
    {
        private readonly string Esquema;

        public MapPerfiles(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("PERFILES", Esquema);

            builder.HasKey(c => c.id)
                .HasName("PERFILES_PKEY");

            builder.Property(c => c.id)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(c => c.ClavePerfil)
                .HasColumnName("CLAVE_PERFIL")
                .HasColumnType("VARCHAR2(500)")
                .IsRequired();

            builder.Property(c => c.NombrePerfil)
                .HasColumnName("NOMBRE_PERFIL")
                .HasColumnType("VARCHAR2(500)")
                .IsRequired();

            builder.Property(c => c.Eliminable)
                .HasColumnName("ELIMINABLE")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(c => c.FechaCreacion)
                .HasColumnName("FECHA_CREACION")
                .HasColumnType("DATE");

            builder.Property(c => c.UsuarioCreacion)
                .HasColumnName("USUARIO_CREACION")
                .HasColumnType("RAW(16)");

            builder.Property(c => c.FechaModificacion)
                .HasColumnName("FECHA_MODIFICACION")
                .HasColumnType("DATE");

            builder.Property(c => c.UsuarioModificacion)
                .HasColumnName("USUARIO_MODIFICACION")
                .HasColumnType("RAW(16)");

            builder.Property(c => c.Activo)
                .HasColumnName("ACTIVO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();
        }
    }
}