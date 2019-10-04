using Cargos.Domain;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Cargos.RepositoryTest
{
    [TestClass]
    public class CargoRepositoryTestCase
    {
        public ICargoRepository CargoRepository { get; set; }
        public IFacturaRepository FacturaRepository { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>().UseLazyLoadingProxies().UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FACTURACION;").Options;

            this.CargoRepository = new CargoRepository(new ApplicationDBContext(options));
            this.FacturaRepository = new FacturaRepository(new ApplicationDBContext(options));
        }

        [TestMethod]
        public void SaveCargo_Ok()
        {
            Factura factura = EntityBuilder.BuildFactura();

            this.FacturaRepository.Save(factura);

            Cargo cargo = EntityBuilder.BuildCargo();

            factura.Cargos.Add(cargo);

            this.FacturaRepository.Update(factura);

            Assert.IsTrue(cargo.Id != 0);
        }
    }
}
