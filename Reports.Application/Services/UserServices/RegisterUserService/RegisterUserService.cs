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
                NationalCode = request.NationalCode.ToString(),
                Position = request.Position,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                UserName = request.Email
            };


            var result = await _userManager.CreateAsync(user,request.Password);
            var addingRoleResult = _userManager.AddToRoleAsync(user, RoleName.User.ToString());

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
