using Cargos.Domain.Base;
using System.Collections.Generic;

namespace Cargos.Domain
{
    public class Factura : Domain<long>
    {
        public long User_Id { get; set; }
        public IList<Cargo> Cargos { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
