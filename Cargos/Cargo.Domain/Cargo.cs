using Cargos.Domain.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cargos.Domain
{
    [Table("Cargo", Schema = "dbo")]
    public class Cargo : Domain<long>
    {
        [Required]
        public long User_Id { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; }

        [Required]
        public StateCargo State { get; set; }

        [Required]
        public TypeCargo Type { get; set; }

        [Required]
        public long EventoId { get; set; }

        [ForeignKey("EventoId")]
        public virtual Evento Evento { get; set; }

        public Cargo()
        {

        }
    }

    public enum TypeCargo
    {
        INDEFINIDO = 0,
        MARKETPLACE = 1,
        SERVICIOS = 2,
        EXTERNO = 3,
    }

    public enum StateCargo
    {
        Deuda = 1,
        Pagado = 2,
    }
}
