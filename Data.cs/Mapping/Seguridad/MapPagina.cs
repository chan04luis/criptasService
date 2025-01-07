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
    public partial class MapPagina : IEntityTypeConfiguration<Pagina>
    {
        public void Configure(EntityTypeBuilder<Pagina> builder)
        {
            // table
            builder.ToTable("paginas");

            // key
            builder.HasKey(e => e.uIdPagina).HasName("PK_Paginas");

            // properties
            builder.Property(e => e.uIdPagina)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.uIdModulo)
                .HasColumnType("uuid")
                .IsUnicode(false)
                .HasColumnName("id_modulo");

            builder.Property(e => e.sClavePagina)
                .HasColumnType("VARCHAR(50)")
                .IsUnicode(false)
                .HasColumnName("clave_pagina");

            builder.Property(e => e.sNombrePagina)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("nombre_pagina");

            builder.Property(e => e.sPathPagina)
                .HasColumnType("VARCHAR(250)")
                .IsUnicode(false)
                .HasColumnName("path_pagina");

            builder.Property(e => e.bMostrarEnMenu)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("mostrar_en_menu");

            builder.Property(e => e.bActivo)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("activo");

            builder.HasOne(c => c.Modulo)
              .WithMany(e => e.lstPaginas)
              .HasForeignKey(c => c.uIdModulo)
              .HasConstraintName("fk_modulos");

            //relaciones
            builder
                .HasOne(c => c.Modulo)
                .WithMany(e => e.lstPaginas)
                .HasForeignKey(c => c.uIdModulo)
                .HasConstraintName("fk_modulos");
        }
    }
}
