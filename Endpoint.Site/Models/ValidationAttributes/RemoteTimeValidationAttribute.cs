using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Models.ValidationAttributes
{
    public class RemoteTimeValidationAttribute : ValidationAttribute
    {

        public bool IsRemote { get; set; }

        public RemoteTimeValidationAttribute(bool isRemote)
        {
            IsRemote = isRemote;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var hourOrMinute = (int?)validationContext.ObjectInstance;
            

            if (hourOrMinute == null)
            {
                return new ValidationResult($"ساعت و دقیقه برای گزارش غیرحضوری الزامیست");
            }

            return ValidationResult.Success;
        }
    }
}
