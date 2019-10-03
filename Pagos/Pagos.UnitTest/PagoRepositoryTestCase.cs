using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pagos.Domain;
using Pagos.Infrastructure;
using Pagos.Infrastructure.Repository;
using Pagos.RepositoryTest;

namespace Pagos.UnitTest
{
    [TestClass]
    public class PagoRepositoryTestCase
    {
        public IPagoRepository PagoRepository { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>().UseLazyLoadingProxies().UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FACTURACION;").Options;

            this.PagoRepository = new PagoRepository(new ApplicationDBContext(options));
        }

        [TestMethod]
        public void SavePago_Ok()
        {
            Pago pago = EntityBuilder.BuildPago();

            this.PagoRepository.Save(pago);

            Assert.IsTrue(pago.Id != 0);
        }
    }
}
