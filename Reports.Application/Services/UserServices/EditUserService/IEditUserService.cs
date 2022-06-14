using Reports.DataAccess.Contexts;
using Reports.DataAccess.Entities.Users;
using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.UserServices.EditUserService
{
    public interface IEditUserService
    {
        ResultDto Execute(UserToEditModelDto request);
    }

    public class EditUserService : IEditUserService
    {
        private readonly Context _context;

        public EditUserService(Context context)
        {
            _context = context;
        }

        public ResultDto Execute(UserToEditModelDto request)
        {
            User user = _context.Users.SingleOrDefault(p => p.Id == request.UserId);
            if (user == null)
                return new ResultDto(false, "کاربر مورد نظر برای ویرایش یافت نشد!");

            User userWithSameInformations = _context.Users
                .Where(p => (p.PhoneNumber == request.PhoneNumber || p.UserName == request.Email || p.NationalCode == request.NationalCode) && p.Id != user.Id)
                .FirstOrDefault();

            if (userWithSameInformations != null)
            {
                if (userWithSameInformations.PhoneNumber == request.PhoneNumber)
                    return new ResultDto(false, $"این شماره موبایل به {userWithSameInformations.FirstName} {userWithSameInformations.LastName} اختصاص یافته است!");                
                if (userWithSameInformations.Email == request.Email)
                    return new ResultDto(false, $"این ایمیل به {userWithSameInformations.FirstName} {userWithSameInformations.LastName} اختصاص یافته است!");
                if (userWithSameInformations.NationalCode == request.NationalCode)
                    return new ResultDto(false, $"این کدملی به {userWithSameInformations.FirstName} {userWithSameInformations.LastName} اختصاص یافته است!");
            }

            user.FirstName = request.FName;
            user.LastName = request.LName;
            user.UserName = request.Email;
            user.Email = request.Email;
            user.NationalCode = request.NationalCode;
            user.Position = request.Position;

            if(user.Email != request.Email)
            {
                user.UserName = request.Email;
                user.Email = request.Email;
                user.EmailConfirmed = false;
            }
            if(user.PhoneNumber != request.PhoneNumber)
            {
                user.PhoneNumber = request.PhoneNumber;
                user.PhoneNumberConfirmed = false;
            }

            _context.SaveChanges();

            return new ResultDto(true, "ویرایش با موفقیت انجام شد!");
        }
    }

    public class UserToEditModelDto
    {
        public string UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NationalCode { get; set; }
        public string Position { get; set; }
    }
}
