using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities;

namespace Data.cs.Mapping
{
    public partial class MapClientes : IEntityTypeConfiguration<Clientes>
    {
        public void Configure(EntityTypeBuilder<Clientes> builder)
        {
            builder.ToTable("clientes");
            builder.HasKey(c => c.uId).HasName("PK_Cliente");

            builder.Property(c => c.uId)
                .HasColumnType("uuid")
                .HasColumnName("id");

            builder.Property(c => c.sNombre)
                .HasColumnType("VARCHAR(255)")
                .IsUnicode(false)
                .HasColumnName("nombre");

            builder.Property(c => c.sApellidos)
                .HasColumnType("VARCHAR(255)")
                .IsUnicode(false)
                .HasColumnName("apellidos");

            builder.Property(c => c.sDireccion)
                .HasColumnType("VARCHAR")
                .IsUnicode(false)
                .HasColumnName("direccion");

            builder.Property(c => c.sTelefono)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("telefono");

            builder.Property(c => c.sEmail)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("email");

            builder.Property(c => c.sSexo)
                .HasColumnType("VARCHAR(10)")
                .HasColumnName("sexo");

            builder.Property(c => c.sFechaNacimiento)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("fecha_nac");

            builder.Property(c => c.dtFechaRegistro)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_registro")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(c => c.dtFechaActualizacion)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_actualizacion")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(c => c.dtFechaEliminado)
                .HasColumnType("timestamp without time zone")
                .IsUnicode(false)
                .HasColumnName("fecha_eliminado")
                .HasConversion(
                    v => DateTime.SpecifyKind(v.ToLocalTime(), DateTimeKind.Unspecified),
                    v => v
                );

            builder.Property(c => c.bEstatus)
                .HasColumnType("bolean")
                .IsUnicode(false)
                .HasColumnName("estatus");

            builder.Property(c => c.bEliminado)
                .HasColumnType("bolean")
                .IsUnicode(false)
                .HasColumnName("eliminado");
        }
    }
}
