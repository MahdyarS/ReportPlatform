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
}
