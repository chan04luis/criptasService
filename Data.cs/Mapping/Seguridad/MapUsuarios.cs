using Data.cs.Entities.Seguridad;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.cs.Mapping.Seguridad
{
    public partial class MapUsuarios : IEntityTypeConfiguration<Usuarios>
    {
        private readonly string Esquema;

        public MapUsuarios(string Esquema)
        {
            this.Esquema = Esquema;
        }

        public void Configure(EntityTypeBuilder<Usuarios> builder)
        {
            builder.ToTable("USUARIOS", Esquema);

            builder.HasKey(u => u.uId).HasName("USUARIOS_PKEY");

            builder.Property(u => u.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(u => u.uIdPerfil)
                .HasColumnName("ID_PERFIL")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(u => u.sNombres)
                .HasColumnName("NOMBRES")
                .HasColumnType("VARCHAR2(100 CHAR)")
                .IsRequired();

            builder.Property(u => u.sApellidos)
                .HasColumnName("APELLIDOS")
                .HasColumnType("VARCHAR2(100 CHAR)");

            builder.Property(u => u.sCorreo)
                .HasColumnName("CORREO")
                .HasColumnType("VARCHAR2(255 CHAR)")
                .IsRequired();

            builder.Property(u => u.sContra)
                .HasColumnName("CONTRA")
                .HasColumnType("CLOB")
                .IsRequired();

            builder.Property(u => u.sTelefono)
                .HasColumnName("TELEFONO")
                .HasColumnType("VARCHAR2(15 CHAR)");

            builder.Property(u => u.bActivo)
                .HasColumnName("ACTIVO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(u => u.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            builder.Property(u => u.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(u => u.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(u => u.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP");

        }
    }
}
