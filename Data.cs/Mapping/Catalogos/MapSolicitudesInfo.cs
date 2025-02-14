using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapSolicitudesInfo : IEntityTypeConfiguration<SolicitudesInfo>
    {
        private readonly string Esquema;

        public MapSolicitudesInfo(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<SolicitudesInfo> builder)
        {
            builder.ToTable("solicitudes_info", Esquema);
            builder.HasKey(u => u.Id).HasName("solicitudes_info_pk");

            builder.Property(u => u.Id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(u => u.IdCliente)
                .HasColumnType("uuid")
                .HasColumnName("id_cliente");

            builder.Property(u => u.IdServicio)
                .HasColumnType("uuid")
                .HasColumnName("id_servicio");

            builder.Property(u => u.Mensaje)
                .HasColumnType("text")
                .HasColumnName("mensaje");

            builder.Property(u => u.Visto)
                .HasColumnType("boolean")
                .HasColumnName("visto");

            builder.Property(u => u.Atendido)
                .HasColumnType("boolean")
                .HasColumnName("atendido");

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
        }
    }
}
