using System.ComponentModel.DataAnnotations;

namespace lib.Extentions;

public class MaxLengthForEach : ValidationAttribute
{
    private readonly int _maxValue;

    public MaxLengthForEach(int value)
    {
        _maxValue = value;
    }
    
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is IEnumerable<string> strings)
        {
            foreach (var str in strings)
            {
                if (str != null && str.Length > _maxValue)
                {
                    return new ValidationResult($"Each string must be up to {_maxValue} characters long.");
                }
            }
            return ValidationResult.Success;
        }

        return new ValidationResult("Invalid input type.");
    }
}