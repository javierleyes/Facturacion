using Cargos.API.DataContract;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Cargos.API.Validator
{
    public class CargoUpdateDataContractValidator : AbstractValidator<CargoUpdateDataContract>
    {
        public CargoUpdateDataContractValidator()
        {
            RuleFor(x => x.Payment_Debt).GreaterThan(0).Must(ValidateAmount)
                .WithMessage("El monto debe ser mayor a 0 y con 2 decimales");

            RuleFor(x => x.Cargo_Id).GreaterThan(0)
                .WithMessage("El id del cargo debe ser mayor a 0");
        }

        private bool ValidateAmount(decimal amount)
        {
            Regex rgx = new Regex(@"^\d+\,\d{2}?$");

            return rgx.IsMatch(amount.ToString());
        }
    }
}
