using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.GetWorkTimeChart
{
    public interface IGetWorkTimeChart
    {
        ResultDto<GetWorkTimeChartResultDto> Execute(GetWorkTimeChartRequestDto request);
    }


}
