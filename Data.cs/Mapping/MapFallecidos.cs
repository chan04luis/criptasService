using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities;

namespace Data.cs.Mapping
{
    public partial class MapFallecidos : IEntityTypeConfiguration<Fallecidos>
    {
        public void Configure(EntityTypeBuilder<Fallecidos> builder)
        {
            builder.ToTable("fallecidos");

            builder.HasKey(e => e.id).HasName("id");

            builder.Property(e => e.id_cirpta)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(e => e.nombre)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("nombre");

            builder.Property(e => e.fecha_fallecimiento)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_fallecimiento");

            builder.Property(e => e.fecha_registro)
                .HasColumnType("DateTime")
                .HasColumnName("fecha_registro");

            builder.Property(e => e.estatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");

        }
    }
}
