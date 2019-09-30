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
        ARS = 1,
        USD = 2,
    }

    public enum TypeEvento
    {
        CLASIFICADO = 1,
        VENTA = 2,
        PUBLICIDAD = 3,
        ENVIO = 4,
        CREDITO = 5,
        MERCADOPAGO = 6,
        MERCADOSHOP = 7,
        FIDELIDAD = 8,
    }
}
