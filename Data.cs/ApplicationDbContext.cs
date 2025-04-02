using Data.cs.Entities.Catalogos;
using Data.cs.Mapping.Seguridad;
using Data.cs.Mapping.Catalogos;
using Microsoft.EntityFrameworkCore;
using Data.cs.Entities.Seguridad;
namespace Data.cs
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #region Catalogo
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Sucursales> Sucursal { get; set; }
        public virtual DbSet<Servicios> Servicios { get; set; }
        public virtual DbSet<ServiciosSucursales> ServiciosSucursales { get; set; }
        public virtual DbSet<ServiciosUsuario> ServiciosUsuario { get; set; }
        public virtual DbSet<SucursalesUsuario> SucursalesUsuario { get; set; }
        #endregion

        #region entities seguridad
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<Perfil> Perfiles { get; set; }
        public virtual DbSet<Configuracion> Configuracion { get; set; }
        public virtual DbSet<Modulo> Modulo { get; set; }
        public virtual DbSet<Pagina> Pagina { get; set; }
        public virtual DbSet<Boton> Boton { get; set; }
        public virtual DbSet<PermisoModulos> PermisosModulos { get; set; }
        public virtual DbSet<PermisosPagina> PermisosPagina { get; set; }
        public virtual DbSet<PermisoBotones> PermisoBotones { get; set; }
        #endregion


        private const string EsquemaCatalogo = "catalogo";

        private const string EsquemaSeguridad = "seguridad";


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            PrepararIgleisas(modelBuilder);
            PrepararSeguridad(modelBuilder);
        }
        private void PrepararIgleisas(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapClientes(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapSucursales(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapServicios(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapServiciosSucursales(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapServiciosUsuario(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapSucursalesUsuario(EsquemaCatalogo));

            OnModelCreatingPartial(modelBuilder);
        }
        private void PrepararSeguridad(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapUsuarios(EsquemaSeguridad));
            modelBuilder.ApplyConfiguration(new MapPerfiles(EsquemaSeguridad));
            modelBuilder.ApplyConfiguration(new MapConfiguracion(EsquemaSeguridad));
            modelBuilder.ApplyConfiguration(new MapModulo(EsquemaSeguridad));
            modelBuilder.ApplyConfiguration(new MapPagina(EsquemaSeguridad));
            modelBuilder.ApplyConfiguration(new MapBoton(EsquemaSeguridad));
            modelBuilder.ApplyConfiguration(new MapPermisoModulos(EsquemaSeguridad));
            modelBuilder.ApplyConfiguration(new MapPermisosPaginas(EsquemaSeguridad));
            modelBuilder.ApplyConfiguration(new MapPermisosBotones(EsquemaSeguridad));

            OnModelCreatingPartial(modelBuilder);

        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
