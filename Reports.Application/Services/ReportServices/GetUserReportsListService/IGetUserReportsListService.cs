using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Reports.Application.Services.ReportServices.GetUserReportsListService
{
    public interface IGetUserReportsListService
    {
        ResultDto<GetUsersReportsResultDto> Execute(GetReportServiceRequestDto request);
    }


}
