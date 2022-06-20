using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.GetAllReportsOfOneDay
{
    public interface IGetAllReportsOfOneDayService
    {
        ResultDto<GetAllReportsOfOneDayResultDto> Execute(GetReportsOfOneDayRequestDto request);
    }


}
