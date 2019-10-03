using Cargos.Domain;
using Cargos.Infraesctructure;
using Cargos.Infrastructure.Repository;
using Cargos.RepositoryTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cargos.RepositoryTestCase
{
    [TestClass]
    public class EventoRepositoryTestCase
    {
        public IEventoRepository EventoRepository { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>().UseLazyLoadingProxies().UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FACTURACION;").Options;

            this.EventoRepository = new EventoRepository(new ApplicationDBContext(options));
        }

        [TestMethod]
        public void SaveEvento_Ok()
        {
            Evento evento = EntityBuilder.BuildEvento();

            this.EventoRepository.Save(evento);

            Assert.IsTrue(evento.Id != 0);
        }
    }
}
