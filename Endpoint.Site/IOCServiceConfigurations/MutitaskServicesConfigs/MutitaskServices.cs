using Reports.Application.Services.MutitaskServices.AdminPanelHomePageService;
using Reports.Application.Services.MutitaskServices.UserPanelHomePageService;

namespace Endpoint.Site.IOCServiceConfigurations.MutitaskServicesConfigs
{
    public static class MutitaskServices
    {
        public static IServiceCollection AddMutitaskServices(this IServiceCollection services)
        {
            services.AddScoped<IUserPanelHomePageService, UserPanelHomePageService>();
            services.AddScoped<IAdminPanelHomePageService, AdminPanelHomePageService>();

            return services;
        }

    }
}
