using System;
using System.Collections.Generic;

namespace Pagos.API.DataContract
{
    public class PagoOutputDataContract
    {
        public long Pago_Id { get; set; }
        public string Currency { get; set; }
        public decimal Amount_Currency { get; set; }
        public decimal Amount_Legal { get; set; }
        public long User_id { get; set; }
        public DateTime Date { get; set; }
        public IList<ConstanciaOutputDataContract> Constancias { get; set; }

        public PagoOutputDataContract()
        {
            this.Constancias = new List<ConstanciaOutputDataContract>();
        }
    }
}
