using Cargos.API.DataContract;
using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace Cargos.API.Validator
{
    public class EventoInputDataContractValidator : AbstractValidator<EventoInputDataContract>
    {
        public EventoInputDataContractValidator()
        {
            RuleFor(x => x.Event_id).NotEmpty().GreaterThan(0)
                .WithMessage("El id del evento debe ser mayor a 0");

            RuleFor(x => x.Amount).GreaterThan(0).Must(ValidateAmount)
                .WithMessage("El monto debe ser mayor a 0 y con 2 decimales");

            RuleFor(x => x.Currency).Must(ValidateCurrency)
                .WithMessage("Los valores válidos para Currency son: USD o ARS");

            RuleFor(x => x.User_id).NotEmpty().GreaterThan(0)
                .WithMessage("El id del usuario debe ser mayor a 0");

            RuleFor(x => x.Event_type).Must(ValidateEventType)
                .WithMessage("Los valores válidos para Event_Type son: CLASIFICADO, VENTA, PUBLICIDAD, ENVIO, CREDITO, MERCADOPAGO, MERCADOSHOP o FIDELIDAD");

            RuleFor(x => x.Date).Must(ValidateDate)
                .WithMessage("La fecha no corresponde al periodo actual");
        }

        private bool ValidateAmount(decimal amount)
        {
            Regex rgx = new Regex(@"^\d+\.\d{2}?$");

            return rgx.IsMatch(amount.ToString());
        }

        private bool ValidateCurrency(string currency)
        {
            Regex rgx = new Regex(@"^(USD|ARS)$");

            return rgx.IsMatch(currency);
        }

        private bool ValidateEventType(string event_type)
        {
            Regex rgx = new Regex(@"^(CLASIFICADO|VENTA|PUBLICIDAD|ENVIO|CREDITO|MERCADOPAGO|MERCADOSHOP|FIDELIDAD)$");

            return rgx.IsMatch(event_type);
        }

        private bool ValidateDate(DateTime date)
        {
            return ((date.Month == DateTime.Now.Month) && (date.Year == DateTime.Now.Year));
        }
    }
}
