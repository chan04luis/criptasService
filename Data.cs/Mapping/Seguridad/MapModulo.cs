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
    public partial class MapModulo : IEntityTypeConfiguration<Modulo>
    {
        private readonly string Esquema;

        public MapModulo(string Esquema)
        {
            this.Esquema = Esquema;
        }
        public void Configure(EntityTypeBuilder<Modulo> builder)
        {
            // table
            builder.ToTable("modulos", Esquema);

            // key
            builder.HasKey(e => e.uIdModulo).HasName("PK_Modulos");

            // properties

            builder.Property(e => e.uIdModulo)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.sClaveModulo)
                .HasColumnType("VARCHAR(50)")
                .IsUnicode(false)
                .HasColumnName("clave_modulo");

            builder.Property(e => e.sNombreModulo)
                .HasColumnType("VARCHAR(500)")
                .IsUnicode(false)
                .HasColumnName("nombre_modulo");

            builder.Property(e => e.sPathModulo)
                .HasColumnType("VARCHAR(250)")
                .IsUnicode(false)
                .HasColumnName("path_modulo");

            builder.Property(e => e.bMostrarEnMenu)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("mostrar_en_menu");

            builder.Property(e => e.bActivo)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("activo");
        }
    }
}
