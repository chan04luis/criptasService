using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapGrupo : IEntityTypeConfiguration<Grupo>
    {
        private readonly string Esquema;

        public MapGrupo(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Grupo> builder)
        {
            builder.ToTable("grupo", Esquema);
            builder.HasKey(g => g.Id).HasName("grupo_pk");

            builder.Property(g => g.Id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(g => g.Nombre)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("nombre");
        }
    }
}
