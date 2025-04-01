using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapCriptas : IEntityTypeConfiguration<Criptas>
    {
        private readonly string Esquema;

        public MapCriptas(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Criptas> builder)
        {
            builder.ToTable("CRIPTAS", Esquema);

            builder.HasKey(c => c.uId)
                .HasName("CRIPTAS_PKEY");

            builder.Property(c => c.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(c => c.uIdSeccion)
                .HasColumnName("ID_SECCION")
                .HasColumnType("RAW(16)");

            builder.Property(c => c.uIdCliente)
                .HasColumnName("ID_CLIENTE")
                .HasColumnType("RAW(16)");

            builder.Property(c => c.sNumero)
                .HasColumnName("NUMERO")
                .HasColumnType("VARCHAR2(50)")
                .IsRequired();

            builder.Property(c => c.sUbicacionEspecifica)
                .HasColumnName("UBICACION_ESPECIFICA")
                .HasColumnType("VARCHAR2(255)");

            builder.Property(c => c.dPrecio)
                .HasColumnName("PRECIO")
                .HasColumnType("NUMBER")
                .IsRequired();

            builder.Property(c => c.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(c => c.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(c => c.dtFechaPagado)
                .HasColumnName("FECHA_PAGADO")
                .HasColumnType("TIMESTAMP");

            builder.Property(c => c.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(c => c.bEstatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(c => c.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(c => c.bDisponible)
                .HasColumnName("DISPONIBLE")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

        }
    }
}
