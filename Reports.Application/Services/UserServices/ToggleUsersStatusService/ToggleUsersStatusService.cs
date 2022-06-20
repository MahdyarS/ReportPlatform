using Microsoft.AspNetCore.Identity;
using Reports.DataAccess.Entities.Users;
using Reports.Helpers.Dtos.ResultDto;

namespace Reports.Application.Services.UserServices.ToggleUsersStatusService
{
    public class ToggleUsersStatusService : IToggleUsersStatusService
    {
        private readonly UserManager<User> _userManager;

        public ToggleUsersStatusService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public ResultDto Execute(string userId)
        {
            User user = _userManager.FindByIdAsync(userId).Result;
            if (user == null)
                return new ResultDto(false, "کاربر مورد نظر یافت نشد!");

            if (user.LockoutEnd < DateTimeOffset.Now || user.LockoutEnd == null)
            {
                _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(new DateTime(2222, 2, 2))).Wait();
                return new ResultDto(true, "کاربر با موفقیت غیرفعال شد!");
            }
            _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.Now).Wait();

            return new ResultDto(true, "کاربر با موفقیت فعال شد!");

        }
    }
}
