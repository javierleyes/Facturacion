using Cargos.Domain;
using System;

namespace Cargos.RepositoryTest
{
    public static class EntityBuilder
    {
        public static Evento BuildEvento()
        {
            return new Evento()
            {
                Amount = Convert.ToDecimal(150.10),
                Currency = Currency.USD,
                Date = DateTime.Now,
                Type = TypeEvento.MERCADOPAGO,
                User_Id = 450,
            };
        }

        public static Cargo BuildCargo()
        {
            return new Cargo()
            {
                Amount = Convert.ToDecimal(150.10),
                Balance = Convert.ToDecimal(150.10),
                State = StateCargo.Deuda,
                Type = TypeCargo.SERVICIOS,
                User_Id = 51,
                Evento = BuildEvento(),
            };
        }

        public static Factura BuildFactura()
        {
            Factura factura = new Factura()
            {
                Month = 9,
                Year = 2019,
                User_Id = 60,
            };

            factura.Cargos.Add(BuildCargo());

            return factura;
        }
    }
}
