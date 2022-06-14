using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Models.AccountControllerModels
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name ="کدملی")]
        public string NationalCode { get; set; }
        [Required]
        [Display(Name ="رمز ورود")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "مرا به خاطر بسپار")]
        public bool IsPersistent { get; set; } = false;
    }
}
