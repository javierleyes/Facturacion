using Cargos.Domain.Base;

namespace Cargos.Domain
{
    public class Cargo : Domain<long>
    {
        public long IdUsuario { get; set; }
        public decimal Monto { get; set; }
        public decimal Saldo { get; set; }
        public EstadoCargo Estado { get; set; }
        public TipoCargo Tipo { get; set; }
        public Evento Evento { get; set; }
    }

    public enum TipoCargo
    {
        MarketPlace = 1,
        Servicios = 2,
        Externo = 3,
    }

    public enum EstadoCargo
    {
        Deuda = 1,
        Pagado = 2,
    }
}
