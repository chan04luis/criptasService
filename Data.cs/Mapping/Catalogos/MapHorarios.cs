using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapHorarios : IEntityTypeConfiguration<Horarios>
    {
        private readonly string Esquema;

        public MapHorarios(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Horarios> builder)
        {
            builder.ToTable("horarios", Esquema);
            builder.HasKey(h => h.Id).HasName("horarios_pk");

            builder.Property(h => h.Id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(h => h.Nombre)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("nombre");

            builder.Property(h => h.HoraInicio)
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("hora_inicio");

            builder.Property(h => h.HoraFin)
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("hora_fin");
        }
    }
}
