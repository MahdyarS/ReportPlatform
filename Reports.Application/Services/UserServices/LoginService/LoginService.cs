using Microsoft.AspNetCore.Identity;
using Reports.DataAccess.Entities.Users;
using Reports.Helpers.Dtos.ResultDto;

namespace Reports.Application.Services.UserServices.LoginService
{
    public class LoginService : ILoginService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LoginService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<ResultDto> Execute(LoginServiceRequestDto request)
        {
            await _signInManager.SignOutAsync();

            if (request == null || String.IsNullOrEmpty(request.UserName) || String.IsNullOrEmpty(request.Password))
                return new ResultDto(false, "اطلاعات وارد شده معتبر نیست!");
            
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                return new ResultDto(false, "اطلاعات وارد شده صحیح نیست!");

            var result = await _signInManager.PasswordSignInAsync(request.UserName,request.Password,true,true);

            if (result.IsLockedOut)
                return new ResultDto(false, "حساب کاربری شما غیر فعال می باشد!");

            if (result.RequiresTwoFactor)
            {
                //
            }

            return new ResultDto(true, "ورود با موفقیت انجام شد!");
        }
    }
}
