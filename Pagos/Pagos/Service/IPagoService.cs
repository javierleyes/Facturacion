using Pagos.API.DataContract;
using System.Collections.Generic;

namespace Pagos.API.Service
{
    public interface IPagoService
    {
        #region POST
        bool CheckPago(PagoInputDataContract input);
        IList<string> GetErrorsCheckPago(PagoInputDataContract input);
        PagoOutputDataContract CreatePago(PagoInputDataContract input);
        #endregion

        #region GET
        PagoOutputDataContract GetPagoById(long id);
        #endregion
    }
}
