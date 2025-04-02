using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Control;

namespace Data.cs.Mapping.Control
{
    public class MapAsistencia : IEntityTypeConfiguration<Asistencia>
    {
        private readonly string Esquema;

        public MapAsistencia(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Asistencia> builder)
        {
            builder.ToTable("asistencia", Esquema);
            builder.HasKey(a => a.Id).HasName("asistencia_pk");

            builder.Property(a => a.Id)
                .HasColumnType("uuid")
                .HasColumnName("Id");

            builder.Property(a => a.IdUsuario)
                .HasColumnType("uuid")
                .HasColumnName("IdUsuario");

            builder.Property(a => a.IdGrupo)
                .HasColumnType("uuid")
                .HasColumnName("IdGrupo");

            builder.Property(a => a.Fecha)
                .HasColumnType("date")
                .HasColumnName("Fecha");

            builder.Property(a => a.Tipo)
                .HasColumnType("varchar(20)")
                .HasColumnName("Tipo");

            builder.Property(a => a.HoraRegistro)
                .HasColumnType("timestamp")
                .HasColumnName("HoraRegistro");
        }
    }
}
