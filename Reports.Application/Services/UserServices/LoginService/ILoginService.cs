using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.UserServices.LoginService
{
    public interface ILoginService
    {
        Task<ResultDto> Execute(LoginServiceRequestDto request);
    }
}
