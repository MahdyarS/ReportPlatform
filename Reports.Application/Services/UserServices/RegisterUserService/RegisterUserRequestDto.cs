namespace Reports.Application.Services.UserServices.RegisterUserService
{
    public class RegisterUserRequestDto
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public string Password { get; set; }
        public string NationalCode { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
