using Reports.Helpers.Dtos.ResultDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.PeriodServices.EditPeriodService
{
    public interface IEditPeriodService
    {
        ResultDto Execute(EditPeriodRequestDto request);
    }
}
