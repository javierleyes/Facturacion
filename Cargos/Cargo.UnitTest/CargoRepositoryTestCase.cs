using Cargos.Domain;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cargos.RepositoryTest
{
    [TestClass]
    public class CargoRepositoryTestCase
    {
        public ICargoRepository CargoRepository { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>().UseLazyLoadingProxies().UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FACTURACION;").Options;

            this.CargoRepository = new CargoRepository(new ApplicationDBContext(options));
        }

        [TestMethod]
        public void SaveCargo_Ok()
        {
            Cargo cargo = EntityBuilder.BuildCargo();

            this.CargoRepository.Save(cargo);

            Assert.IsTrue(cargo.Id != 0);
        }
    }
}
