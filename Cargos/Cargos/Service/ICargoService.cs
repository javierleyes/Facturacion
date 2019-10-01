using Cargos.API.DataContract;
using System.Collections.Generic;

namespace Cargos.API.Service
{
    public interface ICargoService
    {
        // POST
        bool CheckEvento(EventoInputDataContract input);
        IList<string> GetErrorsCheckEvento(EventoInputDataContract input);
        void CreateEvento(EventoInputDataContract input);

        // GET
        CargoOutputDataContract GetCargoById(long id);

        FacturaOutputDataContract GetFacturaById(long id);
    }
}
