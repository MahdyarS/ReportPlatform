namespace Reports.Application.Services.ReportServices.GetWorkTimeChart
{
    public class GetWorkTimeChartRequestDto
    {
        public string UserId { get; set; }
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public string StartDate { get; set; }
        public string FinishDate { get; set; }
    }


}
