using Reports.Application.Services.UserServices.RegisterUserService;
using Reports.Application.Services.UserServices.GetUsersService;
using Reports.Application.Services.UserServices.LoginService;
using Reports.Application.Services.UserServices.LogoutService;

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

            return services;
        }
    }
}
