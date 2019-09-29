using Cargos.Domain.Base;
using System;

namespace Cargos.Domain
{
    public class Evento : Domain<long>
    {
        public long User_Id { get; set; }
        public Currency Currency { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public TypeEvento Type { get; set; }
    }

    public enum Currency
    {
        Peso = 1,
        Dolar = 2,
    }

    public enum TypeEvento
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
