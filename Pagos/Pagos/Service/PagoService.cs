﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

            AddCargo(debt, payment_Amount, pago);

            PagoRepository.Save(pago);

            return this.GetPagoById(pago.Id);
        }

        private void AddCargo(DebtInputDataContract debt, decimal payment_Amount, Pago pago)
        {
            foreach (var cargo in debt.Cargos)
            {
                if (payment_Amount == 0)
                    break;

                Constancia constancia = new Constancia()
                {
                    Cargo_Id = cargo.Cargo_Id,
                };

                if (payment_Amount >= cargo.Amount)
                {
                    constancia.Amount = cargo.Amount;

                    payment_Amount -= cargo.Amount;
                    cargo.Amount = 0;
                }
                else
                {
                    constancia.Amount = payment_Amount;

                    cargo.Amount -= payment_Amount;
                    payment_Amount = 0;
                }







                CargoUpdateDataContract cargo_Update = new CargoUpdateDataContract()
                {
                    Cargo_Id = constancia.Cargo_Id,
                    Payment_Debt = constancia.Amount,
                };



                string uri = "https://localhost:44311/";
                string action = "api/Cargos";

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(uri);

                    var putTask = client.PutAsJsonAsync(action, cargo_Update);
                    putTask.Wait();

                    var result = putTask.Result;

                    if (result.IsSuccessStatusCode)
                    {

                    }
                }





                pago.Constancias.Add(constancia);
            }
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
                Pago_Id = pago.Id,
                Currency = pago.Currency.ToString(),
                Amount_Currency = pago.Amount_Currency,
                Amount_Legal = pago.Amount_Legal,
                User_id = pago.User_Id,
                Constancias = pago.Constancias.Select(x => new ConstanciaOutputDataContract() { Cargo_Id = x.Cargo_Id, Amount = x.Amount }).ToList(),
            };
        }

        #endregion
    }
}
