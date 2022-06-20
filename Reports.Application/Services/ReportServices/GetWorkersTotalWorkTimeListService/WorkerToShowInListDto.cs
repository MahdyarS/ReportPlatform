namespace Reports.Application.Services.ReportServices.GetWorkersTotalWorkTimeListService
{
    public class WorkerToShowInListDto
    {
        public string UsersFirstName { get; set; }
        public string UsersLastName { get; set; }
        public string UserId { get; set; }
        public string WorkTime { get; set; }
        public string RemoteWorkTime { get; set; }
        public string NoneRemoteWorkTime { get; set; }
        public int WorkHour { get; set; }
    }
}
