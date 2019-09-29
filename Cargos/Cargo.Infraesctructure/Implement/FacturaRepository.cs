using Cargos.Domain;
using Cargos.Infrastructure.Base;
using Cargos.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cargos.Infrastructure.Implement
{
    public class FacturaRepository : BaseRepository<Factura>, IFacturaRepository
    {
        public FacturaRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
