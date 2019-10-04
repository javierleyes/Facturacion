using Pagos.Domain;
using System;

namespace Pagos.RepositoryTest
{
    public static class EntityBuilder
    {
        public static Constancia BuildConstancia()
        {
            return new Constancia()
            {
                Amount = Convert.ToDecimal(9006.00),
                Cargo_Id = 3,
            };
        }

        public static Pago BuildPago()
        {
            Pago pago = new Pago()
            {
                Amount_Currency = Convert.ToDecimal(150.10),
                Amount_Legal = Convert.ToDecimal(9006.00),
                Currency = Currency.USD,
                User_Id = 200,
                Date = DateTime.Now,
            };
            pago.Constancias.Add(BuildConstancia());

            return pago;
        }
    }
}
