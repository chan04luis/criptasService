
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapServicios : IEntityTypeConfiguration<Servicios>
    {
        private readonly string Esquema;

        public MapServicios(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Servicios> builder)
        {
            builder.ToTable("servicios", Esquema);
            builder.HasKey(u => u.Id).HasName("servicios_pk");

            builder.Property(u => u.Id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(u => u.Nombre)
                .HasColumnType("VARCHAR(255)") // Ajusta el tamaño si es necesario
                .HasColumnName("nombre");

            builder.Property(u => u.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");

            builder.Property(u => u.Estatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");


            builder.Property(u => u.Eliminado)
                .HasColumnType("boolean")
                .HasColumnName("eliminado");

            builder.Property(u => u.FechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(u => u.FechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(u => u.Img)
                .HasColumnType("text")
                .HasColumnName("img");

            builder.Property(u => u.ImgPreview)
                .HasColumnType("text")
                .HasColumnName("img_preview");
        }
    }
}
