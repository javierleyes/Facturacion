using Pagos.API.DataContract;
using System.Collections.Generic;

namespace Pagos.API.Service
{
    public interface IPagoService
    {
        #region POST
        bool CheckInput(PagoInputDataContract input);
        bool CheckAmountDebt(PagoInputDataContract input, DebtInputDataContract debt);
        IList<string> GetErrorsCheckPago(PagoInputDataContract input);
        PagoOutputDataContract CreatePago(PagoInputDataContract input, DebtInputDataContract debt);
        #endregion

        #region GET
        PagoOutputDataContract GetPagoById(long id);
        #endregion
    }
}
