using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Areas.Admin.Models.UserControllerModels
{
    public class RegisterUserViewModel
    {
        [Required]
        [Display(Name ="نام")]
        public string FName { get; set; }
        [Required]
        [Display(Name = "نام خانوادگی")]
        public string LName { get; set; }
        [Required]
        [Phone]
        [Display(Name ="شماره تلفن")]
        public string PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "کد ملی")]
        public long NationalCode { get; set; }
        [Required]
        [Display(Name = "سمت")]
        public string Position { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "رمز عبور")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "تکرار رمز عبور")]
        public string ConfirmPassword { get; set; }

    }
}
