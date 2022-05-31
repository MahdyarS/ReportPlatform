using Microsoft.AspNetCore.Identity;
using Reports.DataAccess.Entities.Users;
using Reports.Helpers.Dtos.ResultDto;
using Reports.Helpers.UtilityServices.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application.Services.UserServices.GetUsersService
{
    public interface IGetUsersService
    {
        ResultDto<GetUsersResultDto> Execute(GetUsersServiceRequestDto request);
    }

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
                .Where(p => p.FirstName.Contains(request.SearchKey) || p.LastName.Contains(request.SearchKey))
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
                    RequestedPageIndex = request.PageIndex,
                    PagesCount = paginationResult.PagesCount,
                    RequestedSearchKey = request.SearchKey,
                }
            };

            return result;

        }
    }

    public class GetUsersResultDto
    {
        public List<UserModelInAdminList>? UsersList { get; set; }
        public int RequestedPageIndex { get; set; }
        public string RequestedSearchKey { get; set; }
        public bool PrevIsDiabled { get; set; }
        public bool NextIsDisabled { get; set; }
        public int FirstPageIndexToShow { get; set; }
        public int LastPageIndexToShow { get; set; }
        public int PagesCount { get; set; }

    }

    public class GetUsersServiceRequestDto
    {
        public string SearchKey { get; set; } = "";
        public int PageIndex { get; set; } = 1;
        public int ItemsInPageCount { get; set; } = 20;
    }

    public class UserModelInAdminList
    {
        public string FName { get; set; }
        public string LName { get; set; }
        public bool IsDisabled { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string UserId { get; set; }
    }

}
