using System.ComponentModel.DataAnnotations;

namespace MoviesFetcher.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class IMDbIdAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var imdbId = value as string;
            if (!string.IsNullOrEmpty(imdbId) && imdbId.StartsWith("tt", StringComparison.OrdinalIgnoreCase))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult("The IMDbId must start with 'tt' and can not be null.");
        }
    }
}
