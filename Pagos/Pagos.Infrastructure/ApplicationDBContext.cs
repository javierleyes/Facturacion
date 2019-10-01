using Microsoft.EntityFrameworkCore;
using Pagos.Domain;
using System;

namespace Pagos.Infrastructure
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

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        { }

        public DbSet<Pago> Pagos { get; set; }
    }
}
