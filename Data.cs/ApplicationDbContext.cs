using Data.cs.Entities.Catalogos;
using Data.cs.Mapping.Seguridad;
using Data.cs.Mapping.Catalogos;
using Microsoft.EntityFrameworkCore;
using Data.cs.Entities.Seguridad;
using Data.cs.Entities.AtencionMedica;
using Data.cs.Mapping.AtencionMedica;
using Data.cs.Entities.Control;
using Data.cs.Mapping.Control;

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
        public virtual DbSet<Horarios> Horarios { get; set; }
        public virtual DbSet<Dias> Dias { get; set; }
        public virtual DbSet<Validador> Validador { get; set; }
        public virtual DbSet<Tarjeta> Tarjeta { get; set; }
        public virtual DbSet<Grupo> Grupo { get; set; }
        public virtual DbSet<GrupoRelacion> GrupoRelacion { get; set; }
        public virtual DbSet<Asistencia> Asistencia { get; set; }
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

        #region AtencionMedica
        public virtual DbSet<Citas> Citas { get; set; }
        public virtual DbSet<SalaEspera> SalaEspera { get; set; }
        public virtual DbSet<SalaConsulta> SalaConsulta { get; set; }

        #endregion


        private const string EsquemaCatalogo = "catalogo";

        private const string EsquemaSeguridad = "seguridad";

        private const string EsquemaAtencionMedica = "atencion_medica";


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            PrepararIgleisas(modelBuilder);
            PrepararSeguridad(modelBuilder);
            PrepararAtencionMedica(modelBuilder);
        }
        private void PrepararAtencionMedica(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapCitas(EsquemaAtencionMedica));
            modelBuilder.ApplyConfiguration(new MapSalaEspera(EsquemaAtencionMedica));
            modelBuilder.ApplyConfiguration(new MapSalaConsulta(EsquemaAtencionMedica));

            OnModelCreatingPartial(modelBuilder);
        }

        private void PrepararIgleisas(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MapClientes(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapSucursales(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapServicios(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapServiciosSucursales(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapServiciosUsuario(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapSucursalesUsuario(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapDias(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapValidador(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapHorarios(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapTarjeta(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapGrupo(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapGrupoRelacion(EsquemaCatalogo));
            modelBuilder.ApplyConfiguration(new MapAsistencia(EsquemaCatalogo));

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
