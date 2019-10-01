using Pagos.Domain;
using Pagos.Infrastructure.Base;

namespace Pagos.Infrastructure.Repository
{
    public class PagoRepository : BaseRepository<Pago>, IPagoRepository
    {
        public PagoRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
        }
    }
}
