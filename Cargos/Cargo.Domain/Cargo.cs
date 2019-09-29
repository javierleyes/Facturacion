using Cargos.Domain.Base;

namespace Cargos.Domain
{
    public class Cargo : Domain<long>
    {
        public long User_Id { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public StateCargo State { get; set; }
        public TypeCargo Type { get; set; }
        public Evento Event { get; set; }
    }

    public enum TypeCargo
    {
        MarketPlace = 1,
        Servicios = 2,
        Externo = 3,
    }

    public enum StateCargo
    {
        Deuda = 1,
        Pagado = 2,
    }
}
