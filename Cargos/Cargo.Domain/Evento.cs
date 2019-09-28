using Cargo.Domain.Base;
using System;

namespace Cargo.Domain
{
    public class Evento : Domain<long>
    {
        public long IdUsuario { get; set; }
        public Moneda Moneda { get; set; }
        public decimal Monto { get; set; }
        public DateTime Fecha { get; set; }
        public TipoEvento Tipo { get; set; }
    }

    public enum Moneda
    {
        Peso = 1,
        Dolar = 2,
    }

    public enum TipoEvento
    {
        Clasificado = 1,
        Venta = 2,
        Publicidad = 3,
        Envio = 4,
        Credito = 5,
        MercadoPago = 6,
        MercadoShop = 7,
        Fidelidad = 8,
    }
}
