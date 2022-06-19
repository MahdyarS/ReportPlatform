using Endpoint.Site.Models.ReportsControllerModels;
using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Models.ValidationAttributes
{
    public class NoneRemoteEditReportTimeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var report = (EditReportViewModel)validationContext.ObjectInstance;

            if (!report.IsRemote)
            {
                if (value == null)
                    return new ValidationResult("این فیلد الزامیست");
            }

            return ValidationResult.Success;
        }
    }
}
