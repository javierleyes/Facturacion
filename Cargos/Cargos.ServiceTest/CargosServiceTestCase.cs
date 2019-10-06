using Cargos.API.DataContract;
using Cargos.API.Validator;
using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Cargos.ServiceTest
{
    [TestClass]
    public class CargosServiceTestCase
    {
        private IValidator<EventoInputDataContract> EventoInputDataContractValidator { get; set; }
        private IValidator<CargoUpdateDataContract> CargoUpdateDataContractValidator { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.EventoInputDataContractValidator = new EventoInputDataContractValidator();
            this.CargoUpdateDataContractValidator = new CargoUpdateDataContractValidator();
        }

        [TestMethod]
        public void ValidateEventoInput_Ok()
        {
            EventoInputDataContract evento_Input = new EventoInputDataContract()
            {
                Amount = Math.Round(105.60m, 2),
                Currency = "ARS",
                Date = DateTime.Now,
                User_id = 21,
                Event_type = "CLASIFICADO",
            };

            Assert.AreEqual(this.EventoInputDataContractValidator.Validate(evento_Input).IsValid, true);
        }

        [TestMethod]
        public void ValidateEventoInput_Currency_Error()
        {
            EventoInputDataContract evento_Input = new EventoInputDataContract()
            {
                Amount = Math.Round(105.60m, 2),
                Currency = "AS",
                Date = DateTime.Now,
                User_id = 21,
                Event_type = "CLASIFICADO",
            };

            Assert.AreEqual(this.EventoInputDataContractValidator.Validate(evento_Input).IsValid, false);
        }

        [TestMethod]
        public void ValidateEventoInput_EvenType_Error()
        {
            EventoInputDataContract evento_Input = new EventoInputDataContract()
            {
                Amount = Math.Round(105.60m, 2),
                Currency = "AS",
                Date = DateTime.Now,
                User_id = 21,
                Event_type = "CLASIFIO",
            };

            Assert.AreEqual(this.EventoInputDataContractValidator.Validate(evento_Input).IsValid, false);
        }

        [TestMethod]
        public void ValidateEventoInput_User_Error()
        {
            EventoInputDataContract evento_Input = new EventoInputDataContract()
            {
                Amount = Math.Round(105.60m, 2),
                Currency = "AS",
                Date = DateTime.Now,
                User_id = 0,
                Event_type = "CLASIFIO",
            };

            Assert.AreEqual(this.EventoInputDataContractValidator.Validate(evento_Input).IsValid, false);
        }

        [TestMethod]
        public void ValidateEventoInput_Amount_Error()
        {
            EventoInputDataContract evento_Input = new EventoInputDataContract()
            {
                Amount = Convert.ToDecimal(105),
                Currency = "AS",
                Date = DateTime.Now,
                User_id = 0,
                Event_type = "CLASIFIO",
            };

            Assert.AreEqual(this.EventoInputDataContractValidator.Validate(evento_Input).IsValid, false);
        }

        [TestMethod]
        public void ValidateCargoUpdate_Ok()
        {
            CargoUpdateDataContract cargo_Update = new CargoUpdateDataContract()
            {
                Payment_Debt = Math.Round(105.60m, 2),
                Cargo_Id = 1,
            };

            Assert.AreEqual(this.CargoUpdateDataContractValidator.Validate(cargo_Update).IsValid, true);
        }


        [TestMethod]
        public void ValidateCargoUpdate_Amount_Error()
        {
            CargoUpdateDataContract cargo_Update = new CargoUpdateDataContract()
            {
                Payment_Debt = Convert.ToDecimal(100),
                Cargo_Id = 1,
            };

            Assert.AreEqual(this.CargoUpdateDataContractValidator.Validate(cargo_Update).IsValid, false);
        }

        [TestMethod]
        public void ValidateCargoUpdate_Cargo_Error()
        {
            CargoUpdateDataContract cargo_Update = new CargoUpdateDataContract()
            {
                Payment_Debt = Convert.ToDecimal(100),
                Cargo_Id = 0,
            };

            Assert.AreEqual(this.CargoUpdateDataContractValidator.Validate(cargo_Update).IsValid, false);
        }
    }
}
