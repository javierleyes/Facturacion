using Cargos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cargos.Infraesctructure
{
    public class ApplicationDBContext : DbContext
    {
        /*
         * En EF Core debe existir el constructor sin parametros
         * Para iniciar:
         * 1_ Add-Migration InitialMigration
         * 2_ Update-Database
         * 
         * Drop-Database
         */

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        //    => optionsBuilder.UseLazyLoadingProxies().UseSqlServer(myConnectionString);

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        { }

        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
    }
}
