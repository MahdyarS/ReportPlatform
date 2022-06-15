using Microsoft.EntityFrameworkCore;
using Reports.Application.Services.ReportServices.GetWorkTimeChart;
using Reports.DataAccess.Contexts;
using Reports.Helpers.UtilityServices.DateConversionService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.MutitaskServices.AdminPanelHomePageService
{
    public interface IAdminPanelHomePageService
    {
        AdminPanelHomePageViewModel Execute();
    }

    public class AdminPanelHomePageService : IAdminPanelHomePageService
    {
        private readonly Context _context;
        private readonly IGetWorkTimeChart _getWorkTimeChart;

        public AdminPanelHomePageService(Context context, IGetWorkTimeChart getWorkTimeChart)
        {
            _context = context;
            _getWorkTimeChart = getWorkTimeChart;
        }

        public AdminPanelHomePageViewModel Execute()
        {
            var result = new AdminPanelHomePageViewModel
            {
                ChartData = _getWorkTimeChart.Execute(new GetWorkTimeChartRequestDto
                {
                    StartDate = DateTime.Now.AddDays(-10).ConvertMiladiToShamsi(),
                    FinishDate = DateTime.Now.ConvertMiladiToShamsi(),
                }).Data.ChartData,
                LastReportsList = GetLastReports()
            };
            return result;
        }

        private List<ReportToShowInAdminHomeDto> GetLastReports()
        {
            int reportsCount = _context.Reports.Count();

            var query = _context.Reports.Include(p => p.User).AsQueryable();

            if (reportsCount > 10)
            {
                query = query.Skip(reportsCount - 10);
            }

            return query.Select(p => new ReportToShowInAdminHomeDto
            {
                Date = p.Date.ConvertMiladiToShamsi(),
                UsersFirstName = p.User.FirstName,
                UsersLastName = p.User.LastName,
                ReportsDetail = p.ReportsDetail,
                WorkTime = p.FinishWorkTime.Subtract(p.StartWorkTime).ToString("hh':'mm"),
            }).ToList();

        }
    }

    public class ReportToShowInAdminHomeDto
    {
        public string Date { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public string ReportsDetail { get; set; }
        public string WorkTime { get; set; }

    }


    public class AdminPanelHomePageViewModel
    {
        public List<ReportToShowInAdminHomeDto> LastReportsList { get; set; }
        public ChartDataDto ChartData { get; set; }
    }
}
