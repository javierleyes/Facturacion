using Pagos.Domain.Base;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pagos.Domain
{
    public class Pago : Domain<long>
    {
        [Required]
        public long User_Id { get; set; }

        [Required]
        public Currency Currency { get; set; }

        [Required]
        public decimal Amount_Currency { get; set; }

        [Required]
        public decimal Amount_Legal { get; set; }

        [NotMapped]
        public IList<long> Cargo_Id { get; set; }
    }

    public enum Currency
    {
        ARS = 1,
        USD = 2,
    }
}
