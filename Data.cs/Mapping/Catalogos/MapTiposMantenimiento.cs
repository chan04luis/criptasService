using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapTiposMantenimiento : IEntityTypeConfiguration<TipoDeMantenimiento>
    {
        private readonly string Esquema;

        public MapTiposMantenimiento(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<TipoDeMantenimiento> builder)
        {
            builder.ToTable("TIPOS_MANTENIMIENTO", Esquema);

            builder.HasKey(u => u.Id)
                .HasName("TIPOS_MANTENIMIENTO_PK");

            builder.Property(u => u.Id)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(u => u.Nombre)
                .HasColumnName("NOMBRE")
                .HasColumnType("VARCHAR2(255)");

            builder.Property(u => u.Descripcion)
                .HasColumnName("DESCRIPCION")
                .HasColumnType("VARCHAR2(255)");

            builder.Property(u => u.Costo)
                .HasColumnName("COSTO")
                .HasColumnType("NUMBER")
                .IsRequired();

            builder.Property(u => u.Img)
                .HasColumnName("IMG")
                .HasColumnType("CLOB");

            builder.Property(u => u.FechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP");

            builder.Property(u => u.FechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(u => u.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(u => u.Estatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)")
                .IsRequired();
        }
    }
}
