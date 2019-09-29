using Cargos.Domain;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Base;
using Cargos.Infrastructure.Interface;

namespace Cargos.Infrastructure.Implement
{
    public class FacturaRepository : BaseRepository<Factura>, IFacturaRepository
    {
        public FacturaRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
        }
    }
}
