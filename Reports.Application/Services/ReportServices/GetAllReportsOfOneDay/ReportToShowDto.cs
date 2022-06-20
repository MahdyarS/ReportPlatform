namespace Reports.Application.Services.ReportServices.GetAllReportsOfOneDay
{
    public class ReportToShowDto
    {
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public string StartWorkTime { get; set; }
        public string FinishWorkTime { get; set; }
        public string IsRemote { get; set; }
        public string TotalWorkTime { get; set; }
        public string ReportsDescription { get; set; }
    }


}
