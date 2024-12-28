using Data.cs.Entities;
using Data.cs.Entities.Catalogos;
using Data.cs.Entities.Seguridad;
using Data.cs.Mapping;
using Data.cs.Mapping.Seguridad;
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
        public virtual DbSet<TiposDePago> TiposDePagos { get; set; }
        public virtual DbSet<Zonas> Zonas { get; set; }
        public virtual DbSet<Secciones> Secciones { get; set; }
        public virtual DbSet<Visitas> Visitas { get; set; }


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
            modelBuilder.HasDefaultSchema(EsquemaIglesia);

            modelBuilder.ApplyConfiguration(new MapClientes());
            modelBuilder.ApplyConfiguration(new MapIglesias());
            modelBuilder.ApplyConfiguration(new MapZonas());
            modelBuilder.ApplyConfiguration(new MapSecciones());
            modelBuilder.ApplyConfiguration(new MapCriptas());
            modelBuilder.ApplyConfiguration(new MapBeneficiarios());
            modelBuilder.ApplyConfiguration(new MapFallecidos());
            modelBuilder.ApplyConfiguration(new MapPagos());
            modelBuilder.ApplyConfiguration(new MapPagosParciales());
            modelBuilder.ApplyConfiguration(new MapVisitas());
            modelBuilder.ApplyConfiguration(new MapTiposDePago());

            OnModelCreatingPartial(modelBuilder);
        }
        private void PrepararSeguridad(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(EsquemaSeguridad);

            modelBuilder.ApplyConfiguration(new MapUsuarios());
            modelBuilder.ApplyConfiguration(new MapPerfiles());
            modelBuilder.ApplyConfiguration(new MapConfiguracion());
            modelBuilder.ApplyConfiguration(new MapModulo());
            modelBuilder.ApplyConfiguration(new MapPagina());
            modelBuilder.ApplyConfiguration(new MapBoton());
            modelBuilder.ApplyConfiguration(new MapPermisoModulos());
            modelBuilder.ApplyConfiguration(new MapPermisosPaginas());
            modelBuilder.ApplyConfiguration(new MapPermisosBotones());

            OnModelCreatingPartial(modelBuilder);

        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
