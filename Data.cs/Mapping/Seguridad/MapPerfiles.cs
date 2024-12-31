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
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.ToTable("perfiles");
            builder.HasKey(c => c.id).HasName("perfiles_pkey");

            builder.Property(c => c.id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(c => c.ClavePerfil)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("clave_perfil");

            builder.Property(c => c.NombrePerfil)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("nombre_perfil");

            builder.Property(c => c.Eliminable)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("eliminable");

            builder.Property(c => c.FechaCreacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_creacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(c => c.UsuarioCreacion)
                .HasColumnType("uuid")
                .HasColumnName("usuario_creacion");

            builder.Property(c => c.FechaModificacion)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_modificacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(c => c.UsuarioModificacion)
               .HasColumnType("uuid")
               .HasColumnName("usuario_modificacion");

            builder.Property(c => c.Activo)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("activo");
        }
    }
}
