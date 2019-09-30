using Cargos.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Cargos.Domain
{
    public class Cargo : Domain<long>
    {
        [Required]
        public long User_Id { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public StateCargo State { get; set; }

        [Required]
        public TypeCargo Type { get; set; }

        [Required]
        public Evento Event { get; set; }
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
