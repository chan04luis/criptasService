using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapZonas : IEntityTypeConfiguration<Zonas>
    {
        private readonly string Esquema;

        public MapZonas(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Zonas> builder)
        {
            builder.ToTable("ZONAS", Esquema);

            builder.HasKey(z => z.uId)
                .HasName("ZONAS_PKEY");

            builder.Property(z => z.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(z => z.uIdIglesia)
                .HasColumnName("ID_IGLESIA")
                .HasColumnType("RAW(16)");

            builder.Property(z => z.sNombre)
                .HasColumnName("NOMBRE")
                .HasColumnType("VARCHAR2(255)")
                .IsRequired();

            builder.Property(z => z.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP");

            builder.Property(z => z.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(z => z.bEstatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)");

            builder.Property(z => z.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(z => z.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.HasOne(z => z.Iglesia)
                .WithMany(i => i.listZonas)
                .HasForeignKey(z => z.uIdIglesia)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
