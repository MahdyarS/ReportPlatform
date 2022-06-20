using Microsoft.EntityFrameworkCore;
using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.GetReportToEditById
{
    public interface IGetReportToEditById
    {
        ResultDto<ReportToShowForEditDto> Execute(int reportId,string userName);
    }

}
