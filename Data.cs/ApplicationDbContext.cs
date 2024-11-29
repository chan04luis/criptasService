using Data.cs.Entities;
using Data.cs.Mapping;
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
        public virtual DbSet<Usuarios> Usuarios { get; set; }



        private const string EsquemaIglesia = "iglesia";

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            PrepararIgleisas(modelBuilder);
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
            modelBuilder.ApplyConfiguration(new MapUsuarios());

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
