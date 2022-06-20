using Microsoft.EntityFrameworkCore;
using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.ReportServices.EditReportService
{
    public interface IEditReportService
    {
        ResultDto Execute(EditReportRequest request);
    }
}
