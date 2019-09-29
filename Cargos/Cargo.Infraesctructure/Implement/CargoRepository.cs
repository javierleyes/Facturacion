using Cargos.Domain;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Base;
using Cargos.Infrastructure.Interface;

namespace Cargos.Infrastructure.Implement
{
    public class CargoRepository : BaseRepository<Cargo>, ICargoRepository
    {
        public CargoRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
        }
    }
}
