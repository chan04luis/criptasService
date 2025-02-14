using Data.cs.Entities;
using Data.cs.Entities.Catalogos;
using Data.cs.Entities.Seguridad;
using Data.cs.Mapping;
using Data.cs.Mapping.Seguridad;
using Data.cs.Mapping.Catalogos;
using Microsoft.EntityFrameworkCore;

namespace Data.cs
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Beneficiarios> Beneficiarios { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Criptas> Criptas { get; set; }
        public virtual DbSet<Iglesias> Iglesias { get; set; }
        public virtual DbSet<Fallecidos> Fallecidos { get; set; }
        public virtual DbSet<Pagos> Pagos { get; set; }
        public virtual DbSet<PagosParciales> PagosParciales { get; set; }
        public virtual DbSet<TipoDeMantenimiento> TipoDeMantenimiento { get; set; }
        public virtual DbSet<TiposDePago> TiposDePagos { get; set; }
        public virtual DbSet<Zonas> Zonas { get; set; }
        public virtual DbSet<Secciones> Secciones { get; set; }
        public virtual DbSet<Visitas> Visitas { get; set; }
        public virtual DbSet<Servicios> Servicios { get; set; }
        public virtual DbSet<SolicitudesInfo> SolicitudesInfo { get; set; }


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


        private const string EsquemaIglesia = "iglesia";

        private const string EsquemaSeguridad = "seguridad";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            PrepararIgleisas(modelBuilder);
            PrepararSeguridad(modelBuilder);
        }
        private void PrepararIgleisas(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapClientes(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapIglesias(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapZonas(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapSecciones(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapCriptas(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapBeneficiarios(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapFallecidos(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapPagos(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapPagosParciales(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapVisitas(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapTiposDePago(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapTiposMantenimiento(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapServicios(EsquemaIglesia));
            modelBuilder.ApplyConfiguration(new MapSolicitudesInfo(EsquemaIglesia));

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
