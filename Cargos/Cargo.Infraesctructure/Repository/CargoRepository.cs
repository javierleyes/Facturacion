using Cargos.Domain;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Base;

namespace Cargos.Infrastructure.Repository
{
    public class CargoRepository : BaseRepository<Cargo>, ICargoRepository
    {
        public CargoRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
        }
    }
}
