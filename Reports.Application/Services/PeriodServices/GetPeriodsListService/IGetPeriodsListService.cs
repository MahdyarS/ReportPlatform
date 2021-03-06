using Microsoft.EntityFrameworkCore;
using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.PeriodServices.GetPeriodsListService
{
    public interface IGetPeriodsListService
    {
        ResultDto<GetPeriodsListResultDto> Execute(GetPeriodListRequestDto request);
    }
}
