using Data.cs.Entities.Catalogos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapBeneficiarios : IEntityTypeConfiguration<Beneficiarios>
    {
        private readonly string Esquema;

        public MapBeneficiarios(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Beneficiarios> builder)
        {
            builder.ToTable("BENEFICIARIOS", Esquema);

            builder.HasKey(z => z.uId)
                .HasName("BENEFICIARIOS_PKEY");

            builder.Property(z => z.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(z => z.uIdCripta)
                .HasColumnName("ID_CRIPTA")
                .HasColumnType("RAW(16)");

            builder.Property(z => z.sNombre)
                .HasColumnName("NOMBRE")
                .HasColumnType("VARCHAR2(255)")
                .IsRequired();

            builder.Property(z => z.sIneFrente)
                .HasColumnName("INE_FRENTE")
                .HasColumnType("CLOB");

            builder.Property(z => z.sIneReverso)
                .HasColumnName("INE_REVERSO")
                .HasColumnType("CLOB");

            builder.Property(z => z.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP");

            builder.Property(z => z.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(z => z.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(z => z.bEstatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)");

            builder.Property(z => z.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();
        }
    }
}
