using Cargos.API.DataContract;
using System.Collections.Generic;

namespace Cargos.API.Service
{
    public interface ICargoService
    {
        #region POST
        bool CheckFormatEventoInput(EventoInputDataContract evento);
        IList<string> GetErrorsCheckEvento(EventoInputDataContract evento);
        CargoOutputDataContract CreateEvento(EventoInputDataContract evento);
        #endregion

        #region GET
        CargoOutputDataContract GetCargoById(long id);
        FacturaOutputDataContract GetFacturaById(long id);
        DeudaUsuarioOutputDataContract GetDeudaByUser(long id);
        bool UserExist(long id);
        UserOutputDataContract GetStatusUser(long id);
        #endregion

        #region PUT
        bool CheckFormatCargoUpdate(CargoUpdateDataContract cargo_Update);
        bool CheckExistCargo(CargoUpdateDataContract cargo_Update);
        bool CheckStateCargo(CargoUpdateDataContract cargo_Update);
        IList<string> GetErrorsCheckCargoUpdate(CargoUpdateDataContract cargo);
        CargoOutputDataContract UpdateCargo(CargoUpdateDataContract cargo);
        #endregion
    }
}
