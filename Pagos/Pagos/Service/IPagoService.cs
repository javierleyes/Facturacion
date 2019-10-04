using Pagos.API.DataContract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pagos.API.Service
{
    public interface IPagoService
    {
        #region POST
        bool CheckInput(PagoInputDataContract input);
        bool CheckAmountDebt(PagoInputDataContract input, DebtInputDataContract debt);
        IList<string> GetErrorsCheckPago(PagoInputDataContract input);
        Task<DebtInputDataContract> GetDebtByUser(long id);
        Task<PagoOutputDataContract> CreatePago(PagoInputDataContract input, DebtInputDataContract debt);
        #endregion

        #region GET
        PagoOutputDataContract GetPagoById(long id);
        bool UserExist(long id);
        IList<PagoOutputDataContract> GetPagoByUser(long id);
        #endregion
    }
}
