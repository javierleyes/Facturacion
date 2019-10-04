using Cargos.Domain.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cargos.Domain
{
    [Table("Evento", Schema = "dbo")]
    public class Evento : Domain<long>
    {
        [Required]
        public long User_Id { get; set; }

        [Required]
        [Range(1,2)]
        public Currency Currency { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(1,8)]
        public TypeEvento Type { get; set; }

        public Evento()
        {

        }
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
