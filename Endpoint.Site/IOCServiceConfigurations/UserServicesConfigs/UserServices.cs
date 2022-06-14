using Reports.Application.Services.UserServices.RegisterUserService;
using Reports.Application.Services.UserServices.GetUsersService;
using Reports.Application.Services.UserServices.LoginService;
using Reports.Application.Services.UserServices.LogoutService;
using Reports.Application.Services.UserServices.GetUserInformationByIdService;
using Reports.Application.Services.UserServices.EditUserService;
using Reports.Application.Services.UserServices.DeleteUserService;
using Reports.Application.Services.UserServices.ToggleUsersStatusService;

namespace Endpoint.Site.IOCServiceConfigurations.UserServicesConfigs
{
    public static class UserServices
    {
        public static IServiceCollection AddUserServices(this IServiceCollection services)
        {
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IGetUsersService, GetUsersService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILogoutService, LogoutService>();
            services.AddScoped<IGetUserInformationByIdService, GetUserInformationByIdService>();
            services.AddScoped<IEditUserService, EditUserService>();
            services.AddScoped<IDeleteUserService, DeleteUserService>();
            services.AddScoped<IToggleUsersStatusService, ToggleUsersStatusService>();

            return services;
        }
    }
}
