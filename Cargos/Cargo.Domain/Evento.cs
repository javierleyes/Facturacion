using Cargos.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;

namespace Cargos.Domain
{
    public class Evento : Domain<long>
    {
        [Required]
        public long Event_Id { get; set; }

        [Required]
        public long User_Id { get; set; }

        [Required]
        public Currency Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
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
