using Microsoft.AspNetCore.Identity;
using Reports.DataAccess.Entities.Users;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.Pagination;

namespace Reports.Application.Services.UserServices.GetUsersService
{
    public class GetUsersService : IGetUsersService
    {
        UserManager<User> _userManager;

        public GetUsersService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public ResultDto<GetUsersResultDto> Execute(GetUsersServiceRequestDto request)
        {

            var paginationResult = 
                _userManager.Users
                .Where(p => p.FirstName.Contains(request.SearchKey ?? "") || p.LastName.Contains(request.SearchKey))
                .Select(p => new UserModelInAdminList
                {
                    FName = p.FirstName,
                    LName = p.LastName,
                    PhoneNumber = p.PhoneNumber,
                    Position = p.Position,
                    IsDisabled = (p.LockoutEnd > DateTimeOffset.Now && p.LockoutEnabled),
                    UserId = p.Id
                }).ToPaged(request.PageIndex,request.ItemsInPageCount);

            if (!paginationResult.Succeeded)
                return new ResultDto<GetUsersResultDto>(false,paginationResult.Message);

            var result = new ResultDto<GetUsersResultDto>(true,paginationResult.Message)
            {
                Data = new GetUsersResultDto
                {
                    UsersList = paginationResult.RequestedPageList,
                    FirstPageIndexToShow = paginationResult.FirstPageIndexToShow,
                    LastPageIndexToShow = paginationResult.LastPageIndexToShow,
                    NextIsDisabled = paginationResult.NextIsDisabled,
                    PrevIsDiabled = paginationResult.PrevIsDiabled,
                    RequestedPageIndex = paginationResult.RequestedPageIndex,
                    PagesCount = paginationResult.PagesCount,
                    RequestedSearchKey = request.SearchKey,
                }
            };

            return result;

        }
    }

}
