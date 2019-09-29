using Usuarios.Domain;
using Usuarios.Infrastructure.Base;

namespace Usuarios.Infrastructure.Interface
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
    }
}
