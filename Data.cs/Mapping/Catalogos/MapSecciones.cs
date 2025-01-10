using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapSecciones : IEntityTypeConfiguration<Secciones>
    {
        private readonly string Esquema;

        public MapSecciones(string Esquema)
        {
            this.Esquema = Esquema;
        }
        public void Configure(EntityTypeBuilder<Secciones> builder)
        {
            builder.ToTable("secciones", Esquema);
            builder.HasKey(s => s.uId).HasName("PK_Seccion");

            builder.Property(s => s.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(s => s.uIdZona)
                .HasColumnType("uuid")
                .HasColumnName("id_zona");

            builder.Property(s => s.sNombre)
                .HasColumnType("VARCHAR(255)")
                .IsUnicode(false)
                .HasColumnName("nombre");

            builder.Property(s => s.dtFechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(s => s.dtFechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(s => s.bEstatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");

            builder.Property(s => s.bEliminado)
                .HasColumnType("boolean")
                .HasColumnName("eliminado");

            builder.Property(s => s.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_eliminado")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.HasOne(s => s.Zona)
                .WithMany(z => z.listSecciones)
                .HasForeignKey(s => s.uIdZona)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
