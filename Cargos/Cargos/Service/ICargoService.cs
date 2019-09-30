using Cargos.API.DataContract;

namespace Cargos.API.Service
{
    public interface ICargoService
    {
        void CreateEvento(EventoInputDataContract input);
    }
}
