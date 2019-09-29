using Cargos.Domain;
using Cargos.Infrastructure.Base;
using Cargos.Infrastructure.Interface;
using Microsoft.EntityFrameworkCore;

namespace Cargos.Infrastructure.Implement
{
    public class EventoRepository : BaseRepository<Evento>, IEventoRepository
    {
        public EventoRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
