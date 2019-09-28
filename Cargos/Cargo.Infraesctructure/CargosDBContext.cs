using Cargos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cargos.Infraesctructure
{
    public class CargosDBContext : DbContext
    {
        /*
         * En EF Core debe existir el constructor sin parametros
         * Para iniciar:
         * 1_ Add-Migration InitialMigration
         * 2_ Update-Database
         * 
         * Drop-Database
         */

        public CargosDBContext(DbContextOptions<CargosDBContext> options) : base(options)
        { }

        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
    }
}
