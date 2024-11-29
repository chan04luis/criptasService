using Data.cs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.cs.Mapping
{
    public partial class MapUsuarios : IEntityTypeConfiguration<Usuarios>
    {
        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.ToTable("usuarios");
            builder.HasKey(u => u.uId).HasName("PK_Usuario");

            builder.Property(u => u.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(u => u.sNombres)
                .HasColumnType("VARCHAR(100)")
                .IsUnicode(false)
                .HasColumnName("nombres");

            builder.Property(u => u.sApellidos)
                .HasColumnType("VARCHAR(100)")
                .IsUnicode(false)
                .HasColumnName("apellidos");

            builder.Property(u => u.sCorreo)
                .HasColumnType("VARCHAR(255)")
                .IsUnicode(false)
                .HasColumnName("correo");

            builder.Property(u => u.sContra)
                .HasColumnType("TEXT")
                .IsUnicode(false)
                .HasColumnName("contra");

            builder.Property(u => u.sTelefono)
                .HasColumnType("VARCHAR(15)")
                .IsUnicode(false)
                .HasColumnName("telefono");

            builder.Property(u => u.bActivo)
                .HasColumnType("BOOLEAN")
                .HasColumnName("activo");

            builder.Property(u => u.bEliminado)
                .HasColumnType("BOOLEAN")
                .HasColumnName("eliminado");

            builder.Property(u => u.dtFechaRegistro)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(u => u.dtFechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(u => u.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("fecha_eliminado")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );
        }
    }
}
