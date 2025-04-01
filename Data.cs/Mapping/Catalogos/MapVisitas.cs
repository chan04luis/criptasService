using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapVisitas : IEntityTypeConfiguration<Visitas>
    {
        private readonly string Esquema;

        public MapVisitas(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Visitas> builder)
        {
            builder.ToTable("VISITAS", Esquema);

            builder.HasKey(z => z.uId)
                .HasName("VISITAS_PKEY");

            builder.Property(z => z.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(e => e.sNombreVisitante)
                .HasColumnName("NOMBRE_VISITANTE")
                .HasColumnType("VARCHAR2(255)");

            builder.Property(e => e.sMensaje)
                .HasColumnName("MENSAJE_VISITANTE")
                .HasColumnType("VARCHAR2(500)");

            builder.Property(e => e.uIdCriptas)
                .HasColumnName("ID_CRIPTAS")
                .HasColumnType("RAW(16)");

            builder.Property(e => e.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTO") // (conservar tal como está en Oracle, aunque sea un typo)
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(e => e.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(e => e.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(e => e.bEstatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(e => e.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.HasOne(z => z.cripta)
                .WithMany(i => i.listVisitas)
                .HasForeignKey(z => z.uIdCriptas)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}