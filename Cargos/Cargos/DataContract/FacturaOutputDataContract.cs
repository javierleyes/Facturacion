using Cargos.Domain;
using System.Collections.Generic;

namespace Cargos.API.DataContract
{
    public class FacturaOutputDataContract
    {
        public long User_Id { get; set; }
        public IList<long> Cargos { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public FacturaOutputDataContract()
        {
            this.Cargos = new List<long>();
        }
    }
}
