using FluentValidation;
using Pagos.API.DataContract;
using System.Text.RegularExpressions;

namespace Pagos.API.Validator
{
    public class PagoInputDataContractValidator : AbstractValidator<PagoInputDataContract>
    {
        public PagoInputDataContractValidator()
        {
            RuleFor(x => x.Amount).GreaterThan(0).Must(ValidateAmount)
                .WithMessage("El monto debe ser mayor a 0 y con 2 decimales");

            RuleFor(x => x.Currency).Must(ValidateCurrency)
                .WithMessage("Los valores válidos para Currency son: USD o ARS");

            RuleFor(x => x.User_id).GreaterThan(0)
                .WithMessage("El id del usuario debe ser mayor a 0");
        }

        private bool ValidateAmount(decimal amount)
        {
            Regex rgx = new Regex(@"^\d+\,\d{2}?$");

            return rgx.IsMatch(amount.ToString());
        }

        private bool ValidateCurrency(string currency)
        {
            Regex rgx = new Regex(@"^(USD|ARS)$");

            return rgx.IsMatch(currency);
        }
    }
}
