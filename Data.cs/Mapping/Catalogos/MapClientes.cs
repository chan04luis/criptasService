using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Data.cs.Entities.Catalogos;

namespace Data.cs.Mapping.Catalogos
{
    public partial class MapClientes : IEntityTypeConfiguration<Clientes>
    {
        private readonly string Esquema;

        public MapClientes(string esquema)
        {
            Esquema = esquema;
        }

        public void Configure(EntityTypeBuilder<Clientes> builder)
        {
            builder.ToTable("CLIENTES", Esquema);

            builder.HasKey(c => c.uId)
                .HasName("CLIENTES_PKEY");

            builder.Property(c => c.uId)
                .HasColumnName("ID")
                .HasColumnType("RAW(16)")
                .IsRequired();

            builder.Property(c => c.sNombre)
                .HasColumnName("NOMBRE")
                .HasColumnType("VARCHAR2(255)")
                .IsRequired();

            builder.Property(c => c.sApellidos)
                .HasColumnName("APELLIDOS")
                .HasColumnType("VARCHAR2(255)");

            builder.Property(c => c.sDireccion)
                .HasColumnName("DIRECCION")
                .HasColumnType("CLOB");

            builder.Property(c => c.sTelefono)
                .HasColumnName("TELEFONO")
                .HasColumnType("VARCHAR2(20)");

            builder.Property(c => c.sEmail)
                .HasColumnName("EMAIL")
                .HasColumnType("VARCHAR2(255)");

            builder.Property(c => c.sSexo)
                .HasColumnName("SEXO")
                .HasColumnType("VARCHAR2(10)");

            builder.Property(c => c.sContra)
                .HasColumnName("CONTRA")
                .HasColumnType("VARCHAR2(200)");

            builder.Property(c => c.iOrigen)
                .HasColumnName("ORIGEN")
                .HasColumnType("NUMBER")
                .IsRequired();

            builder.Property(c => c.sFcmToken)
                .HasColumnName("SFCMTOKEN")
                .HasColumnType("VARCHAR2(255)");

            builder.Property(c => c.sFechaNacimiento)
                .HasColumnName("FECHA_NAC")
                .HasColumnType("VARCHAR2(20)");

            builder.Property(c => c.dtFechaRegistro)
                .HasColumnName("FECHA_REGISTRO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(c => c.dtFechaActualizacion)
                .HasColumnName("FECHA_ACTUALIZACION")
                .HasColumnType("TIMESTAMP");

            builder.Property(c => c.dtFechaEliminado)
                .HasColumnName("FECHA_ELIMINADO")
                .HasColumnType("TIMESTAMP")
                .IsRequired();

            builder.Property(c => c.bEstatus)
                .HasColumnName("ESTATUS")
                .HasColumnType("NUMBER(1)");

            builder.Property(c => c.bEliminado)
                .HasColumnName("ELIMINADO")
                .HasColumnType("NUMBER(1)")
                .IsRequired();

            // Relaciones (si las deseas habilitar luego)
            // builder.HasMany(c => c.listCriptas).WithOne().HasForeignKey(...);
            // builder.HasMany(c => c.listPagos).WithOne().HasForeignKey(...);
        }
    }
}
