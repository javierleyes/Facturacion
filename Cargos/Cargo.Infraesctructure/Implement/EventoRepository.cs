using Cargos.Domain;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Base;
using Cargos.Infrastructure.Interface;

namespace Cargos.Infrastructure.Implement
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        public EventoRepository(ApplicationDBContext applicationDBContext) : base(applicationDBContext)
        {
        }
    }
}
