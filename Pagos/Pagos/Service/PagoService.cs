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
        public bool CheckInput(PagoInputDataContract input)
        {
            return this.PagoInputDataContractValidator.Validate(input).IsValid;
        }

        public bool CheckAmountDebt(PagoInputDataContract pago, DebtInputDataContract debt)
        {
            decimal payment_Amount = GetLegalAmount(pago.Currency, pago.Amount);

            return (payment_Amount <= debt.Amount) ? true : false;
        }

        public IList<string> GetErrorsCheckPago(PagoInputDataContract pago)
        {
            IList<string> errors = new List<string>();

            var validationResult = this.PagoInputDataContractValidator.Validate(pago);

            foreach (var error in validationResult.Errors)
                errors.Add(error.ToString());

            return errors;
        }

        public PagoOutputDataContract CreatePago(PagoInputDataContract input, DebtInputDataContract debt)
        {
            decimal payment_Amount = GetLegalAmount(input.Currency, input.Amount);

            Pago pago = new Pago()
            {
                User_Id = input.User_id,
                Currency = (Currency)Enum.Parse(typeof(Currency), input.Currency),
                Amount_Currency = input.Amount,
                Amount_Legal = payment_Amount,
            };

            PagoRepository.Save(pago);

            return this.GetPagoById(pago.Id);
        }

        // (1) Consideracion: se contara con un servicio que se encargara de devolver el factor de conversion a ARS.
        private decimal GetLegalAmount(string currency, decimal amount_currency)
        {
            if ((Currency)Enum.Parse(typeof(Currency), currency) != Currency.ARS)
                return amount_currency * CONVERSION_FACTOR;

            return amount_currency;
        }

        #endregion

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
    }
}
