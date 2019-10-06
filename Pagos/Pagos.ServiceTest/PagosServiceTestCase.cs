using FluentValidation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pagos.API.DataContract;
using Pagos.API.Validator;
using System;

namespace Pagos.ServiceTest
{
    [TestClass]
    public class PagosServiceTestCase
    {
        private IValidator<PagoInputDataContract> PagoInputDataContractValidator { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            this.PagoInputDataContractValidator = new PagoInputDataContractValidator();
        }

        [TestMethod]
        public void ValidatePagoInput_Ok()
        {
            PagoInputDataContract pago_input = new PagoInputDataContract()
            {
                Amount = Math.Round(105.60m, 2),
                Currency = "ARS",
                User_id = 2,
            };

            Assert.AreEqual(this.PagoInputDataContractValidator.Validate(pago_input).IsValid, true);
        }

        [TestMethod]
        public void ValidatePagoInput_Amount_Error()
        {
            PagoInputDataContract pago_input = new PagoInputDataContract()
            {
                Amount = Convert.ToDecimal(100),
                Currency = "ARS",
                User_id = 2,
            };

            Assert.AreEqual(this.PagoInputDataContractValidator.Validate(pago_input).IsValid, false);
        }

        [TestMethod]
        public void ValidatePagoInput_Currency_Error()
        {
            PagoInputDataContract pago_input = new PagoInputDataContract()
            {
                Amount = Convert.ToDecimal(100),
                Currency = "AS",
                User_id = 2,
            };

            Assert.AreEqual(this.PagoInputDataContractValidator.Validate(pago_input).IsValid, false);
        }

        [TestMethod]
        public void ValidatePagoInput_User_Error()
        {
            PagoInputDataContract pago_input = new PagoInputDataContract()
            {
                Amount = Convert.ToDecimal(100),
                Currency = "AS",
                User_id = 0,
            };

            Assert.AreEqual(this.PagoInputDataContractValidator.Validate(pago_input).IsValid, false);
        }
    }
}
