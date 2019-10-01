using System;
using System.Collections.Generic;
using FluentValidation;
using Pagos.API.DataContract;
using Pagos.Domain;
using Pagos.Infrastructure.Repository;

namespace Pagos.API.Service
{
    public class PagoService : IPagoService
    {
        private IPagoRepository PagoRepository { get; set; }

        private IValidator<PagoInputDataContract> PagoInputDataContractValidator { get; set; }

        // (1) Consideracion: se contara con un servicio que se encargara de devolver el factor de conversion a ARS.
        private const decimal CONVERSION_FACTOR = 60;

        public PagoService(IPagoRepository pagoRepository, IValidator<PagoInputDataContract> pagoInputDataContractValidator)
        {
            this.PagoRepository = pagoRepository;

            this.PagoInputDataContractValidator = pagoInputDataContractValidator;
        }

        #region POST
        public bool CheckPago(PagoInputDataContract input)
        {
            return this.PagoInputDataContractValidator.Validate(input).IsValid;
        }

        public IList<string> GetErrorsCheckPago(PagoInputDataContract input)
        {
            IList<string> errors = new List<string>();

            var validationResult = this.PagoInputDataContractValidator.Validate(input);

            foreach (var error in validationResult.Errors)
                errors.Add(error.ToString());

            return errors;
        }

        public void CreatePago(PagoInputDataContract input)
        {
            decimal conversionFactor = 1;
            decimal amountLegal;

            // (1) Consideracion: se contara con un servicio que se encargara de devolver el factor de conversion a ARS.
            if ((Currency)Enum.Parse(typeof(Currency), input.Currency) != Currency.ARS)
                conversionFactor = CONVERSION_FACTOR;

            amountLegal = input.Amount * conversionFactor;

            Pago pago = new Pago()
            {
                User_Id = input.User_id,
                Currency = (Currency)Enum.Parse(typeof(Currency), input.Currency),
                Amount_Currency = input.Amount,
                Amount_Legal = amountLegal,
            };

            PagoRepository.Save(pago);

            //return evento;
        }
        #endregion

        #region GET
        #endregion
    }
}
