using Microsoft.EntityFrameworkCore;
using Usuarios.Domain;

namespace Usuarios.Infrastructure
{
    public class UsuariosDBContext : DbContext
    {
        /*
        * En EF Core debe existir el constructor sin parametros
        * Para iniciar:
        * 1_ Add-Migration InitialMigration
        * 2_ Update-Database
        * 
        * Drop-Database
        */

        public UsuariosDBContext(DbContextOptions<UsuariosDBContext> options) : base(options)
        { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
