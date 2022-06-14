namespace Reports.Application.Services.UserServices.LoginService
{
    public class LoginServiceRequestDto
    {
        public string NationalCode { get; set; }
        public string Password { get; set; }
        public bool IsPersistent { get; set; } = false;
    }
}
