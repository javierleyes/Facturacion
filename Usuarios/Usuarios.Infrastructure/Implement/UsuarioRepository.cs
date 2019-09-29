using Microsoft.EntityFrameworkCore;
using Usuarios.Domain;
using Usuarios.Infrastructure.Base;
using Usuarios.Infrastructure.Interface;

namespace Usuarios.Infrastructure.Implement
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
