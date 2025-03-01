using Data.cs.Entities.AtencionMedica;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Mapping.AtencionMedica
{
    public class MapSalaEspera : IEntityTypeConfiguration<SalaEspera>
    {
        private readonly string Esquema;

        public MapSalaEspera(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<SalaEspera> builder)
        {
            builder.ToTable("sala_espera", Esquema);
            builder.HasKey(s => s.Id).HasName("sala_espera_pkey");

            builder.Property(s => s.Id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(s => s.IdSucursal)
                .HasColumnType("uuid")
                .HasColumnName("id_sucursal");

            builder.Property(s => s.IdCita)
                .HasColumnType("uuid")
                .HasColumnName("id_cita")
                .IsRequired(false);

            builder.Property(s => s.IdCliente)
                .HasColumnType("uuid")
                .HasColumnName("id_cliente");

            builder.Property(s => s.Turno)
                .HasColumnType("integer")
                .HasColumnName("turno");

            builder.Property(s => s.FechaIngreso)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_ingreso");

            builder.Property(s => s.FechaLlamado)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_llamado")
                .IsRequired(false);

            builder.Property(s => s.Atendido)
                .HasColumnType("boolean")
                .HasColumnName("atendido");

            builder.Property(s => s.FechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
        }
    }
}
