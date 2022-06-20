using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.DeleteReportService
{
    public interface IDeleteReportService
    {
        ResultDto Execute(int ReportId, string UserName);
    }
}
