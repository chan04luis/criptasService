using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;
using Data.cs.Entities.Seguridad;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapGrupoRelacion : IEntityTypeConfiguration<GrupoRelacion>
    {
        private readonly string Esquema;

        public MapGrupoRelacion(string esquema)
        {
            this.Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<GrupoRelacion> builder)
        {
            builder.ToTable("grupo_relacion", Esquema);
            builder.HasKey(gr => gr.Id).HasName("grupo_relacion_pk");

            builder.Property(gr => gr.Id)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(gr => gr.IdDia)
                .HasColumnType("int")
                .HasColumnName("id_dia");

            builder.Property(gr => gr.IdAula)
                .HasColumnType("uuid")
                .HasColumnName("id_aula");

            builder.Property(gr => gr.IdHorario)
                .HasColumnType("uuid")
                .HasColumnName("id_horario");

            builder.Property(gr => gr.IdGrupo)
                .HasColumnType("uuid")
                .HasColumnName("id_grupo");

            builder.Property(gr => gr.IdUsuario)
                .HasColumnType("uuid")
                .HasColumnName("id_usuario");
        }
    }
}
