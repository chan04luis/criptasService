using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapSolicitudesInfo : IEntityTypeConfiguration<SolicitudesInfo>
    {
        private readonly string Esquema;

        public MapSolicitudesInfo(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<SolicitudesInfo> builder)
        {
            builder.ToTable("SOLICITUDES_INFO", Esquema);

            builder.HasKey(u => u.Id)
                .HasName("SOLICITUDES_INFO_PK");

            builder.Property(u => u.Id)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(u => u.IdCliente)
                .HasColumnName("ID_CLIENTE")
                .HasColumnType("RAW(16)");

            builder.Property(u => u.IdServicio)
                .HasColumnName("ID_SERVICIO")
                .HasColumnType("RAW(16)");

            builder.Property(u => u.Mensaje)
                .HasColumnName("MENSAJE")
                .HasColumnType("CLOB");

            builder.Property(u => u.Visto)
                .HasColumnName("VISTO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(u => u.Atendido)
                .HasColumnName("ATENDIDO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(u => u.Eliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(u => u.FechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP");

            builder.Property(u => u.FechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

        }
    }
}
