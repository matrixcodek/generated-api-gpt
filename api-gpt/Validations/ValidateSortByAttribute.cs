using System.ComponentModel.DataAnnotations;

namespace api_gpt.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class ValidateSortByAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value?.ToString()?.ToLower() == "ascend" || value?.ToString()?.ToLower() == "descend")
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("The sortBy filter must be either 'ascend' or 'descend' only");
        }
    }
}
