using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.GetWorkersTotalWorkTimeListService
{
    public interface IGetWorkersTotalWorkTimeListService
    {
        ResultDto<WorkersTotalWorkTimeResultDto> Execute(WorkersTotalWorkTimeRequestDto request);
    }
}
