using System.ComponentModel.DataAnnotations;

namespace Ploomes.API.Validators
{
    public class AgeCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var dataAtual = DateTime.Now.Date;
            var date = (DateTime)value;
            var MinDate = dataAtual.AddYears(-90);

            var MaxDate = dataAtual.AddYears(-18);
            if (date.Between(MinDate, MaxDate))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(GetErrorMessage());
        }

        public static string GetErrorMessage()
        {
            return $"Idade não permitida!";
        }
    }


    public static class DateHelper
    {
        public static bool Between(this DateTime currentDate, DateTime minValue, DateTime maxValue)
        {
            return currentDate.CompareTo(minValue) >= 1 && maxValue.CompareTo(currentDate) >= 1;
        }

        public static bool ValidarHoraDia(this DateTime date)
        {
            return (date.Hour >= 7 && date.Hour < 22);
        }
    }
}
