using Reports.DataAccess.Contexts;
using Reports.Helpers.Dtos.ResultDto;

namespace Reports.Application.Services.UserServices.GetUserInformationByIdService
{
    public class GetUserInformationByIdService : IGetUserInformationByIdService
    {
        private readonly Context _context;

        public GetUserInformationByIdService(Context context)
        {
            _context = context;
        }

        public ResultDto<UserInformationDto> Execute(string UserId)
        {
            var user = _context.Users.Select(p => new UserInformationDto
            {
                Id = p.Id,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                FName = p.FirstName,
                LName = p.LastName,
                NationalCode = p.NationalCode,
                IsConfirmedEmail = p.EmailConfirmed,
                Position = p.Position
            }).SingleOrDefault(p => p.Id == UserId);

            if (user == null)
                return new ResultDto<UserInformationDto>(false, "کاربر یافت نشد!");
            return new ResultDto<UserInformationDto>(true, "")
            {
                Data = user
            };

        }
    }
}
