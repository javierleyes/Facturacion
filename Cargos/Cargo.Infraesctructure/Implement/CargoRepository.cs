using Cargos.Domain;
using Cargos.Infrastructure.Base;
using Cargos.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cargos.Infrastructure.Implement
{
    public class CargoRepository : BaseRepository<Cargo>, ICargoRepository
    {
        public CargoRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
