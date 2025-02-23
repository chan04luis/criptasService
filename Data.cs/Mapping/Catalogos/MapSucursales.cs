using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapSucursales : IEntityTypeConfiguration<Sucursales>
    {
        public readonly string Esquema;
        public MapSucursales(string Esquema)
        {
            this.Esquema = Esquema;
        }
        public void Configure(EntityTypeBuilder<Sucursales> builder)
        {
            builder.ToTable("sucursales", Esquema);
            builder.HasKey(i => i.uId).HasName("PK_Sucursales");

            builder.Property(i => i.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(i => i.sNombre)
                .HasColumnType("VARCHAR(255)")
                .IsUnicode(false)
                .HasColumnName("nombre");

            builder.Property(i => i.sTelefono)
                .HasColumnType("VARCHAR(20)")
                .IsUnicode(false)
                .HasColumnName("telefono");

            builder.Property(i => i.sDireccion)
                .HasColumnType("TEXT")
                .HasColumnName("direccion");

            builder.Property(i => i.sLatitud)
                .HasColumnType("VARCHAR(255)")
                .IsUnicode(false)
                .HasColumnName("latitud");

            builder.Property(i => i.sLongitud)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("longitud");

            builder.Property(i => i.sCiudad)
                .HasColumnType("VARCHAR(100)")
                .HasColumnName("ciudad");

            builder.Property(i => i.dtFechaRegistro)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(i => i.dtFechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(i => i.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_eliminado")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(i => i.bEstatus)
                .HasColumnType("bolean")
                .IsUnicode(false)
                .HasColumnName("estatus");

            builder.Property(i => i.bEliminado)
                .HasColumnType("bolean")
                .IsUnicode(false)
                .HasColumnName("eliminado");
        }
    }
}
