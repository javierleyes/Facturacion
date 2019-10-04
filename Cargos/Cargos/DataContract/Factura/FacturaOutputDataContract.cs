using Cargos.Domain;
using System.Collections.Generic;

namespace Cargos.API.DataContract
{
    public class FacturaOutputDataContract
    {
        public long Factura_Id { get; set; }
        public long User_Id { get; set; }
        public IList<long> Cargos_Id { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public FacturaOutputDataContract()
        {
            this.Cargos_Id = new List<long>();
        }
    }
}
