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

        public virtual DbSet<Iglesias> Iglesias { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<Zonas> Zonas { get; set; }
        public virtual DbSet<Secciones> Secciones { get; set; }

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

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
