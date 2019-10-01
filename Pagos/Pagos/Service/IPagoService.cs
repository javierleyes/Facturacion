using Pagos.API.DataContract;
using System.Collections.Generic;

namespace Pagos.API.Service
{
    public interface IPagoService
    {
        #region POST
        bool CheckPago(PagoInputDataContract input);
        IList<string> GetErrorsCheckPago(PagoInputDataContract input);
        void CreatePago(PagoInputDataContract input);
        #endregion
    }
}
