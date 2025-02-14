using Data.cs.Entities.Catalogos;
using Data.cs.Entities.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Data.cs.Mapping.Catalogos
{
    public partial class MapTiposMantenimiento : IEntityTypeConfiguration<TipoDeMantenimiento>
    {
        private readonly string Esquema;
        public MapTiposMantenimiento(string Esquema)
        {
            this.Esquema = Esquema;
        }
        public void Configure(EntityTypeBuilder<TipoDeMantenimiento> builder)
        {
            builder.ToTable("tipos_mantenimiento", Esquema);
            builder.HasKey(u => u.Id).HasName("tipos_mantenimiento_pk");

            builder.Property(u => u.Id)
           .HasColumnType("uuid")
           .HasColumnName("id");

            builder.Property(u => u.Nombre)
           .HasColumnType("VARCHAR(100)")
           .HasColumnName("nombre");

            builder.Property(u => u.Descripcion)
            .HasColumnType("text")
            .HasColumnName("descripcion");

            builder.Property(u => u.Costo)
           .HasColumnType("numeric")
           .HasColumnName("costo");

            builder.Property(u => u.Estatus)
           .HasColumnType("boolean")
           .HasColumnName("estatus");

            builder.Property(u => u.bEliminado)
            .HasColumnType("boolean")
            .HasColumnName("eliminado");

            builder.Property(u => u.Img)
           .HasColumnType("text")
           .HasColumnName("img");

            builder.Property(u => u.FechaActualizacion)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("fecha_actualizacion")
            .HasConversion(
                v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                v => v
            );

            builder.Property(u => u.FechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );
        }
    }
}
