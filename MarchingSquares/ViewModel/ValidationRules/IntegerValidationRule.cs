using System.Globalization;
using System.Windows.Controls;

namespace MarchingSquares.ViewModel.ValidationRules;

public class IntegerValidationRule : ValidationRule
{
    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        int result;
        if (!int.TryParse(value?.ToString(), out result))
        {
            return new ValidationResult(false, "Input must be a valid integer.");
        }
        return ValidationResult.ValidResult;
    }
}