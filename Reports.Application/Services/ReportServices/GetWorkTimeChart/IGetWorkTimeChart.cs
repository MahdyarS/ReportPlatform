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

            var WorkTimes = query.Select(p => new { Date = p.Date, WorkTimeInDay = new TimeSpan(0,p.TotalWorkedMinutes,0) }).ToList();


            var WorkTimeGroup = (from n in WorkTimes
                                 group n by n.Date into groupedWorkTimes
                                 orderby groupedWorkTimes.Key
                                 select groupedWorkTimes).ToList();
            

            List<string> chartDates = new List<string>();
            List<int> chartWorkTimes = new List<int>();

            double totalMinutes = 0;

            foreach (var item in WorkTimeGroup)
            {
                string date = item.Key.ConvertMiladiToShamsi();
                date = date.Substring(5);

                chartDates.Add(date);

                totalMinutes = 0;

                foreach (var workTime in item)
                {
                    totalMinutes += workTime.WorkTimeInDay.TotalMinutes;
                }

                int totalHours = (int)totalMinutes / 60;
                if (totalMinutes % 60 > 30)
                    totalHours++;
                chartWorkTimes.Add(totalHours);
            }
            var chartData = new ChartDataDto();

            chartData.Dates = chartDates.ToArray();
            chartData.WorkTimes = chartWorkTimes.ToArray();

            return new ResultDto<GetWorkTimeChartResultDto>(true, "")
            {
                Data = new GetWorkTimeChartResultDto
                {
                    ChartData = chartData,
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
    }

    public class ChartDataDto
    {
        public string[] Dates { get; set; }
        public int[] WorkTimes { get; set; }
    }


}
