using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapIglesias : IEntityTypeConfiguration<Iglesias>
    {
        public readonly string Esquema;
        public MapIglesias(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Iglesias> builder)
        {
            builder.ToTable("IGLESIAS", Esquema);

            builder.HasKey(i => i.uId)
                .HasName("IGLESIAS_PKEY");

            builder.Property(i => i.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(i => i.sNombre)
                .HasColumnName("NOMBRE")
                .HasColumnType("VARCHAR2(255)")
                .IsRequired();

            builder.Property(i => i.sDireccion)
                .HasColumnName("DIRECCION")
                .HasColumnType("CLOB");

            builder.Property(i => i.sCiudad)
                .HasColumnName("CIUDAD")
                .HasColumnType("VARCHAR2(100)");

            builder.Property(i => i.sLatitud)
                .HasColumnName("LATITUD")
                .HasColumnType("VARCHAR2(100)");

            builder.Property(i => i.sLongitud)
                .HasColumnName("LONGITUD")
                .HasColumnType("VARCHAR2(100)");

            builder.Property(i => i.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(i => i.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(i => i.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(i => i.bEstatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)")
                .HasConversion(v => v.Value ? 1 : 0, v => v == 1);

            builder.Property(i => i.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .HasConversion(v => v ? 1 : 0, v => v == 1);


        }
    }
}
