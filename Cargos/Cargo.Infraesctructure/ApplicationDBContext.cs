﻿using Cargos.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cargos.Infraesctructure
{
    public class ApplicationDBContext : DbContext
    {
        /*
         * 1_ Add-Migration InitialMigration
         * 2_ Update-Database
         * 
         * Drop-Database
         */

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        { }

        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Evento> Eventos { get; set; }
        public DbSet<Factura> Facturas { get; set; }
    }
}
