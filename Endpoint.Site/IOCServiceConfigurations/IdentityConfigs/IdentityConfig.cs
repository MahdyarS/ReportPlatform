using Reports.DataAccess.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Entities.Users;

namespace Endpoint.Site.IOCServiceConfigurations.IdentityConfigs
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddIdentity<User, Role>()
            .AddEntityFrameworkStores<Context>()
            .AddDefaultTokenProviders()
            .AddRoles<Role>()
            .AddErrorDescriber<IdentityCustomErrors>();


            services.Configure<IdentityOptions>(options =>
            {
                //Lockout
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);

                //SignIn
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;

                //Token
                options.Tokens.EmailConfirmationTokenProvider = "";

                //...
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 8;

                

            });

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromDays(365);
                options.LoginPath = "/login";
            });


            return services;
        }
    }
}
