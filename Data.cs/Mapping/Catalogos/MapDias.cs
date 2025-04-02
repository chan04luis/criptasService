using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapDias : IEntityTypeConfiguration<Dias>
    {
        private readonly string Esquema;

        public MapDias(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Dias> builder)
        {
            builder.ToTable("dias", Esquema);
            builder.HasKey(d => d.Id).HasName("dias_pk");

            builder.Property(d => d.Id)
                .HasColumnType("SERIAL")
                .HasColumnName("id");

            builder.Property(d => d.Nombre)
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("nombre");
        }
    }
}
