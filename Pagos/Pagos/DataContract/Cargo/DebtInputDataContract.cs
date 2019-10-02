using System.Collections.Generic;

namespace Pagos.API.DataContract
{
    public class DebtInputDataContract
    {
        public decimal Amount { get; set; }
        public IList<CargoInputDataContract> Cargos { get; set; }

        public DebtInputDataContract()
        {
            this.Cargos = new List<CargoInputDataContract>();
        }
    }
}
