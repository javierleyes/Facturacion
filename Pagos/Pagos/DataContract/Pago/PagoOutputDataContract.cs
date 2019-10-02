using System.Collections.Generic;

namespace Pagos.API.DataContract
{
    public class PagoOutputDataContract
    {
        public long Pago_Id { get; set; }
        public decimal Amount_Currency { get; set; }
        public decimal Amount_Legal { get; set; }
        public string Currency { get; set; }
        public long User_id { get; set; }
        public IList<long> Cargos_Id { get; set; }

        public PagoOutputDataContract()
        {
            this.Cargos_Id = new List<long>();
        }
    }
}
