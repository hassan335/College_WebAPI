using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models.Validator
{
    public class DateCheck:ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            DateTime d = (DateTime)value;

            if (d> DateTime.Now)
            {
                return new ValidationResult("Date Must be Greater");
            }

            return ValidationResult.Success;
        }

    }
}
