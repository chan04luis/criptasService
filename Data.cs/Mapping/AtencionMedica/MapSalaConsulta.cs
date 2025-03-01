using Data.cs.Entities.AtencionMedica;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Mapping.AtencionMedica
{
    public class MapSalaConsulta : IEntityTypeConfiguration<SalaConsulta>
    {
        private readonly string Esquema;

        public MapSalaConsulta(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<SalaConsulta> builder)
        {
            builder.ToTable("sala_consulta", Esquema);
            builder.HasKey(s => s.Id).HasName("sala_consulta_pkey");

            builder.Property(s => s.Id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(s => s.IdSucursal)
                .HasColumnType("uuid")
                .HasColumnName("id_sucursal");

            builder.Property(s => s.IdDoctor)
                .HasColumnType("uuid")
                .HasColumnName("id_doctor");

            builder.Property(s => s.FechaEntrada)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_entrada");

            builder.Property(s => s.FechaSalida)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_salida")
                .IsRequired(false);

            builder.Property(s => s.FechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro");
        }
    }
}
