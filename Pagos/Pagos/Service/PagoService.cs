using System;
using System.Collections.Generic;
using System.Linq;
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

        public PagoOutputDataContract CreatePago(PagoInputDataContract input)
        {
            decimal conversion_Factor = 1;
            decimal amount_Legal;

            // (1) Consideracion: se contara con un servicio que se encargara de devolver el factor de conversion a ARS.
            if ((Currency)Enum.Parse(typeof(Currency), input.Currency) != Currency.ARS)
                conversion_Factor = CONVERSION_FACTOR;

            amount_Legal = input.Amount * conversion_Factor;

            Pago pago = new Pago()
            {
                User_Id = input.User_id,
                Currency = (Currency)Enum.Parse(typeof(Currency), input.Currency),
                Amount_Currency = input.Amount,
                Amount_Legal = amount_Legal,
            };

            PagoRepository.Save(pago);

            return this.GetPagoById(pago.Id);
        }

        #region GET
        public PagoOutputDataContract GetPagoById(long id)
        {
            Pago pago = this.PagoRepository.GetById(id);

            if (pago == null)
                return null;

            return new PagoOutputDataContract()
            {
                Currency = pago.Currency.ToString(),
                Amount_Currency = pago.Amount_Currency,
                Amount_Legal = pago.Amount_Legal,
                User_id = pago.User_Id,
                Cargos_Id = pago.Cargos_Id.Select(x => x.Cargo_Id).ToList(),
            };
        }
        #endregion

        #endregion
    }
}
