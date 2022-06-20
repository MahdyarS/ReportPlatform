using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.PeriodServices.DeletePeriodService
{
    public interface IDeletePeriodService
    {
        ResultDto Execute(int PeriodId, string UserName);
    }
}
