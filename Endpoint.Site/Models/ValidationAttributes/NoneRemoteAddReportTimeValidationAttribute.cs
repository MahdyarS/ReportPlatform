using Endpoint.Site.Models.ReportsControllerModels;
using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Models.ValidationAttributes
{
    public class NoneRemoteAddReportTimeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var report = (ReportToAddViewModel)validationContext.ObjectInstance;

            if (!report.IsRemote)
            {
                if (value == null)
                    return new ValidationResult("این فیلد الزامیست");
            }

            return ValidationResult.Success;

        }
    }
}
