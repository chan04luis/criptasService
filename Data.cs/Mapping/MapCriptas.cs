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

            builder.HasKey(e => e.id).HasName("id_Criptas");

            builder.Property(e => e.id_seccion)
                .HasColumnType("uuid")
                .HasColumnName("id_seccion");

            builder.Property(e => e.id_clientes)
                .HasColumnType("uuid")
                .HasColumnName("id_clientes");

            builder.Property(e => e.numero)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("numero");

            builder.Property(e => e.ubicacion_especifica)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("ubicacion_especifica");

            builder.Property(e => e.fecha_registro)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_registro");

            builder.Property(e => e.fecha_actualizacion)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_actualizacion");

            builder.Property(e => e.estatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");
        }
    }
}
