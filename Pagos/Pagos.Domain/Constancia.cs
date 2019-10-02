using Pagos.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pagos.Domain
{
    [Table("Constancia", Schema = "dbo")]
    public class Constancia : Domain<long>
    {
        [Required]
        public long Cargo_Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public Constancia()
        {

        }
    }
}
