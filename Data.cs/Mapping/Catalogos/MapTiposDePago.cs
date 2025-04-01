using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapTiposDePago : IEntityTypeConfiguration<TiposDePago>
    {
        private readonly string Esquema;

        public MapTiposDePago(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<TiposDePago> builder)
        {
            builder.ToTable("TIPOS_PAGO", Esquema);

            builder.HasKey(z => z.uId)
                .HasName("TIPOS_PAGO_PKEY");

            builder.Property(z => z.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(e => e.sNombre)
                .HasColumnName("NOMBRE")
                .HasColumnType("VARCHAR2(100)")
                .IsRequired();

            builder.Property(e => e.sDescripcion)
                .HasColumnName("DESCRIPCION")
                .HasColumnType("CLOB");

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP");

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(e => e.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

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
