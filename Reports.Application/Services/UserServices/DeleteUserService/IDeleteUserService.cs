using Reports.DataAccess.Contexts;
using Reports.DataAccess.Entities.Users;
using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.UserServices.DeleteUserService
{
    public interface IDeleteUserService
    {
        ResultDto Execute(string userId);
    }

    public class DeleteUserService : IDeleteUserService
    {
        private readonly Context _context;

        public DeleteUserService(Context context)
        {
            _context = context;
        }

        public ResultDto Execute(string userId)
        {
            User user = _context.Users.SingleOrDefault(p => p.Id == userId);
            if (user == null)
                return new ResultDto(false, "کاربر مورد نظر یافت نشد!");

            user.IsRemoved = true;
            _context.SaveChanges();

            return new ResultDto(true, "کاربر مورد نظر با موفقیت حذف شد!");
        }
    }
}
