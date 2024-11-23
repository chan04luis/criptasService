using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities;

namespace Data.cs.Mapping
{
    public partial class MapCriptas : IEntityTypeConfiguration<Criptas>
    {
        public void Configure(EntityTypeBuilder<Criptas> builder)
        {
            builder.ToTable("criptas");
            builder.HasKey(c => c.uId).HasName("PK_Criptas");

            builder.Property(c => c.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(c => c.uIdSeccion)
                .HasColumnType("uuid")
                .HasColumnName("id_seccion");

            builder.Property(c => c.uIdCliente)
                .HasColumnType("uuid")
                .HasColumnName("id_cliente");

            builder.Property(c => c.sNumero)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("numero");

            builder.Property(c => c.sUbicacionEspecifica)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("ubicacion_especifica");

            builder.Property(c => c.dtFechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(c => c.dtFechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(c => c.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_eliminado")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(c => c.bEstatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");

            builder.Property(c => c.bEliminado)
                .HasColumnType("boolean")
                .HasColumnName("eliminado");

            builder.HasOne(z => z.Cliente)
                .WithMany(i => i.listCriptas)
                .HasForeignKey(z => z.uIdCliente)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(z => z.Seccion)
                .WithMany(i => i.listCriptas)
                .HasForeignKey(z => z.uIdSeccion)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

