using Cargos.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cargos.Domain
{
    [Table("Factura", Schema = "dbo")]
    public class Factura : Domain<long>
    {
        [Required]
        public long User_Id { get; set; }

        public virtual IList<Cargo> Cargos { get; set; }

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
