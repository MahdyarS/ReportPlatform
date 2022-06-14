using Microsoft.AspNetCore.Identity;
using Reports.DataAccess.Entities.Users;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.Enums;

namespace Reports.Application.Services.UserServices.RegisterUserService
{
    public class RegisterUserService : IRegisterUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public RegisterUserService(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ResultDto> Execute(RegisterUserRequestDto request)
        {
            var user = new User
            {
                FirstName = request.FName,
                LastName = request.LName,
                NationalCode = request.NationalCode,
                Position = request.Position,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.Email
            };


            User userWithSameInformations = _userManager.Users
                .Where(p => p.PhoneNumber == request.PhoneNumber || p.UserName == request.Email || p.NationalCode == request.NationalCode)
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


            var result = await _userManager.CreateAsync(user,request.Password);
            var addingRoleResult = await _userManager.AddToRoleAsync(user, RoleName.User.ToString());

            if (result.Succeeded)
                return new ResultDto(true, "ثبت کاربر با موفقیت انجام شد!");

            string message = "";

            foreach (var item in result.Errors.ToList())
            {
                message += item.Description + "\n";
            }
            return new ResultDto(false, message);
        }
    }
}
