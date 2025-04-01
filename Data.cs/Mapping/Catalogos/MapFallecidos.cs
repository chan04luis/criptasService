using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapFallecidos : IEntityTypeConfiguration<Fallecidos>
    {
        private readonly string Esquema;

        public MapFallecidos(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Fallecidos> builder)
        {
            builder.ToTable("FALLECIDOS", Esquema);

            builder.HasKey(z => z.uId)
                .HasName("FALLECIDOS_PKEY");

            builder.Property(z => z.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(c => c.uIdCripta)
                .HasColumnName("ID_CRIPTA")
                .HasColumnType("RAW(16)");

            builder.Property(e => e.sNombre)
                .HasColumnName("NOMBRE")
                .HasColumnType("VARCHAR2(255)")
                .IsRequired();

            builder.Property(e => e.sApellidos)
                .HasColumnName("APELLIDOS")
                .HasColumnType("VARCHAR2(255)");

            builder.Property(e => e.sActaDefuncion)
                .HasColumnName("ACTA_DEFUNCION")
                .HasColumnType("CLOB");

            builder.Property(e => e.sAutorizacionIncineracion)
                .HasColumnName("AUTORIZACION_INCINERACION")
                .HasColumnType("CLOB");

            builder.Property(e => e.sAutorizacionTraslado)
                .HasColumnName("AUTORIZACION_TRASLADO")
                .HasColumnType("CLOB");

            builder.Property(e => e.dtFechaFallecimiento)
                .HasColumnName("FECHA_FALLECIMIENTO")
                .HasColumnType("VARCHAR2(100)");

            builder.Property(e => e.dtFechaNacimiento)
                .HasColumnName("FECHA_NACIMIENTO")
                .HasColumnType("VARCHAR2(100)");

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(e => e.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP");

            builder.Property(e => e.bEstatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)");

            builder.Property(e => e.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

        }
    }
}
