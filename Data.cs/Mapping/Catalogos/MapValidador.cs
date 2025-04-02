using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapValidador : IEntityTypeConfiguration<Validador>
    {
        private readonly string Esquema;

        public MapValidador(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Validador> builder)
        {
            builder.ToTable("validador", Esquema);
            builder.HasKey(v => v.Id).HasName("validador_pk");

            builder.Property(v => v.Id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(v => v.Nombre)
                .HasColumnType("VARCHAR(250)")
                .HasColumnName("nombre");

            builder.Property(v => v.Estado)
                .HasColumnType("BOOLEAN")
                .HasColumnName("estado");

            builder.Property(v => v.UltimaConexion)
                .HasColumnType("TIMESTAMP WITHOUT TIME ZONE")
                .HasColumnName("ultima_conexion");

            builder.Property(v => v.Eliminado)
                .HasColumnType("BOOLEAN")
                .HasColumnName("eliminado");

            builder.Property(v => v.IdAula)
                .HasColumnType("uuid")
                .HasColumnName("id_aula");
        }
    }
}
