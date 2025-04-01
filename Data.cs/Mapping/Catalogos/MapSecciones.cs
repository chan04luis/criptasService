using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapSecciones : IEntityTypeConfiguration<Secciones>
    {
        private readonly string Esquema;

        public MapSecciones(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Secciones> builder)
        {
            builder.ToTable("SECCIONES", Esquema);

            builder.HasKey(s => s.uId)
                .HasName("SECCIONES_PKEY");

            builder.Property(s => s.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(s => s.uIdZona)
                .HasColumnName("ID_ZONA")
                .HasColumnType("RAW(16)");

            builder.Property(s => s.sNombre)
                .HasColumnName("NOMBRE")
                .HasColumnType("VARCHAR2(255)")
                .IsRequired();

            builder.Property(s => s.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(s => s.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(s => s.bEstatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(s => s.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(s => s.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.HasOne(s => s.Zona)
                .WithMany(z => z.listSecciones)
                .HasForeignKey(s => s.uIdZona)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
