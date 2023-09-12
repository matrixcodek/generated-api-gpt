using System.ComponentModel.DataAnnotations;

namespace api_gpt.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
        AllowMultiple = false)]
    public class ValidateSortByAttribute : ValidationAttribute
    {

        private const string AscendValue = "ascend";
        private const string DescendValue = "descend";
        private const string ErrorMessageForUser = "The sortBy filter must be either 'ascend' or 'descend' only";
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value is string stringValue && IsSortByValid(stringValue))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(ErrorMessageForUser);
        }

        private static bool IsSortByValid(string sortBy)
        {
            sortBy = sortBy.ToLower();
            return string.IsNullOrEmpty(sortBy) || sortBy.Equals(AscendValue) || sortBy.Equals(DescendValue);
        }
    }
}
