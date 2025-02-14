using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapFallecidos : IEntityTypeConfiguration<Fallecidos>
    {
        private readonly string Esquema;

        public MapFallecidos(string Esquema)
        {
            this.Esquema = Esquema;
        }
        public void Configure(EntityTypeBuilder<Fallecidos> builder)
        {
            builder.ToTable("fallecidos", Esquema);

            builder.HasKey(z => z.uId)
                .HasName("PK_fallecidos");

            builder.Property(z => z.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(c => c.uIdCripta)
                .HasColumnType("uuid")
                .HasColumnName("id_cripta");

            builder.Property(e => e.sNombre)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("nombre");

            builder.Property(e => e.sApellidos)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("apellidos");

            builder.Property(e => e.dtFechaFallecimiento)
                .HasColumnType("date")
                .HasColumnName("fecha_fallecimiento");

            builder.Property(e => e.dtFechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fecha_nacimiento");

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_eliminado")
                .HasConversion(v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                v => v);

            builder.Property(e => e.bEstatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");

            builder.Property(e => e.bEliminado)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("eliminado");

            builder.HasOne(z => z.cripta)
               .WithMany(i => i.listFallecidos)
               .HasForeignKey(z => z.uIdCripta)
               .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
