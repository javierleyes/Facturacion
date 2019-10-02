using System.Collections.Generic;

namespace Cargos.API.DataContract
{
    public class DeudaUsuarioOutputDataContract
    {
        public decimal Amount { get; set; }
        public IList<CargoOutputDataContract> Cargos { get; set; }

        public DeudaUsuarioOutputDataContract()
        {
            this.Cargos = new List<CargoOutputDataContract>();
        }
    }
}
