using System.Collections.Generic;

namespace Pagos.API.DataContract
{
    public class PagoOutputDataContract
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public long User_id { get; set; }
        public IList<long> Cargos { get; set; }

        public PagoOutputDataContract()
        {
            this.Cargos = new List<long>();
        }
    }
}
