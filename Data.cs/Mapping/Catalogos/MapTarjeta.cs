using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapTarjeta : IEntityTypeConfiguration<Tarjeta>
    {
        private readonly string Esquema;

        public MapTarjeta(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Tarjeta> builder)
        {
            builder.ToTable("tarjeta", Esquema);
            builder.HasKey(t => t.Id).HasName("tarjeta_pk");

            builder.Property(t => t.Id)
                .HasColumnType("VARCHAR(50)")
                .HasColumnName("id");

            builder.Property(t => t.IdUsuario)
                .HasColumnType("UUID")
                .HasColumnName("id_usuario");

            builder.Property(t => t.Estado)
                .HasColumnType("BOOLEAN")
                .HasColumnName("estado");
        }
    }
}
