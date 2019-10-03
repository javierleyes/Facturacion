using Cargos.Domain;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cargos.RepositoryTest
{
    [TestClass]
    public class FacturaRepositoryTestCase
    {
        public IFacturaRepository FacturaRepository { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>().UseLazyLoadingProxies().UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FACTURACION;").Options;

            this.FacturaRepository = new FacturaRepository(new ApplicationDBContext(options));
        }

        [TestMethod]
        public void SaveFactura_Ok()
        {
            Factura factura = EntityBuilder.BuildFactura();

            this.FacturaRepository.Save(factura);

            Assert.IsTrue(factura.Id != 0);
        }
    }
}
