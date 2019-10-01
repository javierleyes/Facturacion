using Cargos.API.DataContract;
using System.Collections.Generic;

namespace Cargos.API.Service
{
    public interface ICargoService
    {
        #region POST
        bool CheckEvento(EventoInputDataContract input);
        IList<string> GetErrorsCheckEvento(EventoInputDataContract input);
        CargoOutputDataContract CreateEvento(EventoInputDataContract input);
        #endregion

        #region GET
        CargoOutputDataContract GetCargoById(long id);
        FacturaOutputDataContract GetFacturaById(long id);
        #endregion
    }
}
