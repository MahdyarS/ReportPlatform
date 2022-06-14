﻿using System.ComponentModel.DataAnnotations;

namespace Endpoint.Site.Areas.Admin.Models.UserControllerModels
{
    public class UserToEditViewModel
    {
        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "این فیلد الزامیست!")]
        [Display(Name = "نام")]
        public string FName { get; set; }

        [Required(ErrorMessage = "این فیلد الزامیست!")]
        [Display(Name = "نام خانوادگی")]
        public string LName { get; set; }

        [Required(ErrorMessage = "این فیلد الزامیست!")]
        [Phone(ErrorMessage = "شماره را صحیح وارد کنید!")]
        [Display(Name = "شماره تلفن")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "این فیلد الزامیست!")]
        [EmailAddress(ErrorMessage = "ایمیل را صحیح وارد کنید")]
        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Required(ErrorMessage = "این فیلد الزامیست!")]
        [StringLength(10, ErrorMessage = "کدملی 10 رقمی است!")]
        [RegularExpression(@"^(?!(\d)\1{9})\d{10}$", ErrorMessage = "کدملی وارد شده صحیح نیست!")]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Required(ErrorMessage = "این فیلد الزامیست!")]
        [Display(Name = "سمت")]
        public string Position { get; set; }
    }
}
