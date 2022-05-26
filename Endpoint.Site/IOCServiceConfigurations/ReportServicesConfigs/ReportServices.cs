using Reports.Application.Services.ReportServices.AddNewReportService;
using Reports.Application.Services.ReportServices.DeleteReportService;
using Reports.Application.Services.ReportServices.EditReportService;
using Reports.Application.Services.ReportServices.GetReportToEditById;
using Reports.Application.Services.ReportServices.GetUserReportsListService;

namespace Endpoint.Site.IOCServiceConfigurations.ReportServicesConfigs
{
    public static class ReportServices
    {
        public static IServiceCollection AddReportServices(this IServiceCollection services)
        {
            services.AddScoped<IAddNewReportService, AddNewReportService>();
            services.AddScoped<IGetUserReportsListService, GetUserReportsListService>();
            services.AddScoped<IEditReportService, EditReportService>();
            services.AddScoped<IGetReportToEditById, GetReportToEditById>();
            services.AddScoped<IDeleteReportService, DeleteReportService>();

            return services;
        }
    }
}
