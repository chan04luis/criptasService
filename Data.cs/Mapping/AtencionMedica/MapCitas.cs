using Data.cs.Entities.AtencionMedica;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Mapping.AtencionMedica
{
    public class MapCitas : IEntityTypeConfiguration<Citas>
    {
        private readonly string Esquema;

        public MapCitas(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Citas> builder)
        {
            builder.ToTable("citas", Esquema);
            builder.HasKey(c => c.Id).HasName("citas_pkey");

            builder.Property(c => c.Id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(c => c.IdCliente)
                .HasColumnType("uuid")
                .HasColumnName("id_cliente");

            builder.Property(c => c.IdSucursal)
                .HasColumnType("uuid")
                .HasColumnName("id_sucursal");

            builder.Property(c => c.IdServicio)
                .HasColumnType("uuid")
                .HasColumnName("id_servicio");

            builder.Property(c => c.IdDoctor)
                .HasColumnType("uuid")
                .HasColumnName("id_doctor")
                .IsRequired(false);

            builder.Property(c => c.FechaCita)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_cita");

            builder.Property(c => c.FechaLlegada)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_llegada")
                .IsRequired(false);

            builder.Property(c => c.FechaSalida)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_salida")
                .IsRequired(false);

            builder.Property(c => c.Estado)
                .HasColumnType("varchar(50)")
                .HasColumnName("estado");

            builder.Property(c => c.Turno)
                .HasColumnType("integer")
                .HasColumnName("turno");

            builder.Property(c => c.RegistradoEnPiso)
                .HasColumnType("boolean")
                .HasColumnName("registrado_en_piso");

            builder.Property(c => c.FechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");

            builder.Property(c => c.FechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion");
        }
    }
}
