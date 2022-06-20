namespace Reports.Application.Services.UserServices.EditUserService
{
    public class UserToEditModelDto
    {
        public string UserId { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string NationalCode { get; set; }
        public string Position { get; set; }
    }
}
