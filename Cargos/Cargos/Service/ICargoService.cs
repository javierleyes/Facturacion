using Cargos.API.DataContract;
using System.Collections.Generic;

namespace Cargos.API.Service
{
    public interface ICargoService
    {
        bool CheckEvento(EventoInputDataContract input);
        IList<string> GetErrorsCheckEvento(EventoInputDataContract input);
        void CreateEvento(EventoInputDataContract input);
    }
}
