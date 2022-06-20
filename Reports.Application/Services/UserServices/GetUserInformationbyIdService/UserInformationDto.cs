namespace Reports.Application.Services.UserServices.GetUserInformationByIdService
{
    public class UserInformationDto
    {
        public string Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalCode { get; set; }
        public bool IsConfirmedEmail { get; set; }
    }
}
