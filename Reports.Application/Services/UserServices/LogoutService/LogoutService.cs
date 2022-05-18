using Microsoft.AspNetCore.Identity;
using Reports.DataAccess.Entities.Users;

namespace Reports.Application.Services.UserServices.LogoutService
{
    public class LogoutService : ILogoutService
    {
        private readonly SignInManager<User> _signInManager;

        public LogoutService(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task Execute()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
