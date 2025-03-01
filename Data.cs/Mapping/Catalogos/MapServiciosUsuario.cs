using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Mapping.Catalogos
{
    public class MapServiciosUsuario : IEntityTypeConfiguration<ServiciosUsuario>
    {
        private readonly string Esquema;

        public MapServiciosUsuario(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<ServiciosUsuario> builder)
        {
            builder.ToTable("usuarios_servicios", Esquema);
            builder.HasKey(u => u.Id).HasName("usuarios_servicios_pkey");

            builder.Property(u => u.Id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(u => u.IdServicio)
                .HasColumnType("uuid")
                .HasColumnName("servicio_id");

            builder.Property(u => u.IdUsuario)
                .HasColumnType("uuid")
                .HasColumnName("usuario_id");

            builder.Property(u => u.Asignado)
                .HasColumnType("boolean")
                .HasColumnName("asignado");

            builder.Property(u => u.FechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_asociacion")
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
        }
    }
}
