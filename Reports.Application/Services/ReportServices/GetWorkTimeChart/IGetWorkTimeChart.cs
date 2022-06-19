using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.DateConversionService;
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

    public class GetWorkTimeChart : IGetWorkTimeChart
    {
        private readonly Context _context;

        public GetWorkTimeChart(Context context)
        {
            _context = context;
        }

        public ResultDto<GetWorkTimeChartResultDto> Execute(GetWorkTimeChartRequestDto request)
        {
            DateTime startDate;
            DateTime finishDate;

            if (String.IsNullOrEmpty(request.FinishDate) || String.IsNullOrEmpty(request.StartDate))
            {
                startDate = DateTime.Now.AddDays(-7);
                finishDate = DateTime.Now;

                request.StartDate = startDate.ConvertMiladiToShamsi();
                request.FinishDate = finishDate.ConvertMiladiToShamsi();
            }
            else
            {
                startDate = request.StartDate.ShamsiStringToDateTime();
                finishDate = request.FinishDate.ShamsiStringToDateTime();

                if (startDate > finishDate)
                    return new ResultDto<GetWorkTimeChartResultDto>(false, "تاریخ پایان بازه نمی تواند قبل از پایان آن باشد!");

            }
            
            var query = _context.Reports.Where(p => p.Date >= startDate && p.Date <= finishDate);

            if (!String.IsNullOrEmpty(request.UserId))
                query = query.Where(p => p.UserId == request.UserId);

            var WorkTimes = query.Select(p => new { Date = p.Date, TotalWorkedMinutes = p.TotalWorkedMinutes,p.IsRemote });


            var WorkTimeGroup = WorkTimes.GroupBy(p => p.Date).Select(p => new
            {
                Date = p.Key,
                TotalWorkedMinutes = p.Sum(p =>p.TotalWorkedMinutes),
                RemoteWorkedMinutes = p.Where(p => p.IsRemote).Sum(p => p.TotalWorkedMinutes)
            }).ToList();
            

            List<string> chartDates = new List<string>();


            List<int> totalWorkChartWorkTimes = new List<int>();
            List<int> remoteWorkChartWorkTimes = new List<int>();
            List<int> noneRemoteWorkChartWorkTimes = new List<int>();


            foreach (var item in WorkTimeGroup)
            {
                string date = item.Date.ConvertMiladiToShamsi();
                date = date.Substring(5);

                chartDates.Add(date);

                int totalHours = item.TotalWorkedMinutes / 60;
                int remoteHours = item.RemoteWorkedMinutes / 60;
                int noneRemoteHours = (item.TotalWorkedMinutes - item.RemoteWorkedMinutes) / 60;

                if (item.TotalWorkedMinutes % 60 > 30)
                    totalHours++;
                if (item.RemoteWorkedMinutes % 60 > 30)
                    remoteHours++;
                if ((item.TotalWorkedMinutes - item.RemoteWorkedMinutes) % 60 > 30)
                    noneRemoteHours++;

                totalWorkChartWorkTimes.Add(totalHours);
                remoteWorkChartWorkTimes.Add(remoteHours);
                noneRemoteWorkChartWorkTimes.Add(noneRemoteHours);
            }
            var chartData = new ChartDataDto();
            var remoteChartData = new ChartDataDto();
            var noneRemoteChartData = new ChartDataDto();

            chartData.Dates = chartDates.ToArray();
            chartData.WorkTimes = totalWorkChartWorkTimes.ToArray();

            remoteChartData.Dates = chartDates.ToArray();
            remoteChartData.WorkTimes = remoteWorkChartWorkTimes.ToArray();

            noneRemoteChartData.Dates = chartDates.ToArray();
            noneRemoteChartData.WorkTimes = noneRemoteWorkChartWorkTimes.ToArray();

            return new ResultDto<GetWorkTimeChartResultDto>(true, "")
            {
                Data = new GetWorkTimeChartResultDto
                {
                    ChartData = chartData,
                    RemoteChartData = remoteChartData,
                    NoneRemoteChartData = noneRemoteChartData,
                    UserId = request.UserId,
                    FinishDate = request.FinishDate,
                    StartDate = request.StartDate,
                    UsersFirstName = request.UsersFirstName,
                    UsersLastName = request.UsersLastName
                }
            };

        }
    }

    public class GetWorkTimeChartRequestDto
    {
        public string UserId { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
    }

    public class GetWorkTimeChartResultDto
    {
        public string UserId { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public ChartDataDto ChartData { get; set; }
        public ChartDataDto RemoteChartData { get; set; }
        public ChartDataDto NoneRemoteChartData { get; set; }
    }

    public class ChartDataDto
    {
        public string[] Dates { get; set; }
        public int[] WorkTimes { get; set; }
    }


}
