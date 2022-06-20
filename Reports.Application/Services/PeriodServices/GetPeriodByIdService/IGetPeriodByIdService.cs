using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.PeriodServices.GetPeriodByIdService
{
    public interface IGetPeriodByIdService
    {
        ResultDto<PeriodToShowDto> Execute(int periodId, string userName);
    }
}
