using Cargos.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cargos.Domain
{
    public class Factura : Domain<long>
    {
        [Required]
        public long User_Id { get; set; }
        public IList<Cargo> Cargos { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        public Factura()
        {
            Cargos = new List<Cargo>();
        }
    }
}
