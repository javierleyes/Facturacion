using Cargos.Domain;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Base;

namespace Cargos.Infrastructure.Repository
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        public EventoRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
        }
    }
}
