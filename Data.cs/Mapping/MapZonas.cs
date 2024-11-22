using Data.cs.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.cs.Mapping
{
    public partial class MapZonas : IEntityTypeConfiguration<Zonas>
    {
        public void Configure(EntityTypeBuilder<Zonas> builder)
        {
            builder.ToTable("zonas");
            builder.HasKey(z => z.uId).HasName("PK_Zona");

            builder.Property(z => z.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(z => z.uIdIglesia)
                .HasColumnType("uuid")
                .HasColumnName("id_iglesia");

            builder.Property(z => z.sNombre)
                .HasColumnType("VARCHAR(255)")
                .IsUnicode(false)
                .HasColumnName("nombre");

            builder.Property(z => z.dtFechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(z => z.dtFechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(z => z.bEstatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");

            builder.Property(z => z.bEliminado)
                .HasColumnType("boolean")
                .HasColumnName("eliminado");

            builder.Property(z => z.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_eliminado")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );
            builder.HasOne(z => z.Iglesia)  
                .WithMany(i => i.listZonas)   
                .HasForeignKey(z => z.uIdIglesia)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
