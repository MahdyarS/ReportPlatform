using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.MutitaskServices.UserPanelHomePageService
{
    public interface IUserPanelHomePageService
    {
        UserPanelHomePageViewModel Execute(string userId);
    }
}
