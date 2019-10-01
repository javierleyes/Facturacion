using Cargos.Domain;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Base;

namespace Cargos.Infrastructure.Repository
{
    public class FacturaRepository : BaseRepository<Factura>, IFacturaRepository
    {
        public FacturaRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
        }
    }
}
