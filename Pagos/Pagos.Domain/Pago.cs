using Pagos.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pagos.Domain
{
    [Table("Pago", Schema = "dbo")]
    public class Pago : Domain<long>
    {
        [Required]
        public long User_Id { get; set; }

        [Required]
        [Range(1,2)]
        public Currency Currency { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount_Currency { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount_Legal { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual IList<Constancia> Constancias { get; set; }

        public Pago()
        {
            this.Constancias = new List<Constancia>();
        }
    }

    public enum Currency
    {
        ARS = 1,
        USD = 2,
    }
}
