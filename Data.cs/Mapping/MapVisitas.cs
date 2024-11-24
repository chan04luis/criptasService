using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.cs.Mapping
{
    public partial class MapVisitas : IEntityTypeConfiguration<Visitas>
    {
        public void Configure(EntityTypeBuilder<Visitas> builder)
        {
            builder.ToTable("Visitas");
            builder.HasKey(e => e.uId).HasName("Id");

            builder.Property(e => e.sNombreVisitante)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("nombre_visitante");

            builder.Property(e => e.uIdCriptas)
                .HasColumnType("uuid")
                .HasColumnName("id_criptas");

            builder.Property(e => e.dtFechaVisita)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_visita")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(e => e.dtFechaResgistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registo")
                .HasConversion(v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                v => v);

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnType("timestamp winthout time zone")
                .HasColumnName("fecha_actualizacion")
                .HasConversion(v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified), v => v);

            builder.Property(e => e.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_eliminado")
                .HasConversion(v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified), v => v);

            builder.Property(e => e.bEstatus)
                .HasColumnType("boolean")
                .HasColumnName("estatus");

            builder.Property(e => e.bEliminado)
                .HasColumnType("boolean")
                .IsUnicode(false)
                .HasColumnName("eliminado");
        }
    }
}
