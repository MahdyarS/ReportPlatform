using Reports.Application.Services.PeriodServices.AddNewPeriodService;
using Reports.Application.Services.PeriodServices.DeletePeriodService;
using Reports.Application.Services.PeriodServices.EditPeriodService;
using Reports.Application.Services.PeriodServices.GetPeriodByIdService;
using Reports.Application.Services.PeriodServices.GetPeriodsListService;

namespace Endpoint.Site.IOCServiceConfigurations.PeriodServicesConfigs
{
    public static class PeriodServices
    {
        public static IServiceCollection AddPeriodServices(this IServiceCollection services)
        {
            services.AddScoped<IAddNewPeriodService, AddNewPeriodService>();
            services.AddScoped<IGetPeriodsListService, GetPeriodsListService>();
            services.AddScoped<IDeletePeriodService, DeletePeriodService>();
            services.AddScoped<IGetPeriodByIdService, GetPeriodByIdService>();
            services.AddScoped<IEditPeriodService, EditPeriodService>();

            return services;
        }
    }
}
