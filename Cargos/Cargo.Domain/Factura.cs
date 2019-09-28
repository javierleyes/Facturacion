using Cargos.Domain.Base;
using System.Collections.Generic;

namespace Cargos.Domain
{
    public class Factura : Domain<long>
    {
        public long IdUsuario { get; set; }

        public IList<Cargo> Cargos { get; set; }

        public int Mes { get; set; }

        public int Anio { get; set; }
    }
}
