using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reports.DataAccess.Entities.Reports;
using Reports.DataAccess.Entities.Users;
using Reports.DataAccess.EntityConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.DataAccess.Contexts
{
    public class Context : IdentityDbContext<User, Role, string>
    {
        public Context(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Report> Reports { get; set; }
        public DbSet<Period> Periods { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityUserLogin<string>>().HasKey(p => new { p.LoginProvider, p.ProviderKey });
            builder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
            builder.Entity<IdentityUserToken<string>>().HasKey(p => new { p.UserId, p.LoginProvider, p.Name });



            builder.ApplyConfiguration(new ReportConfigs());
            builder.ApplyConfiguration(new RoleConfigs());
            builder.ApplyConfiguration(new PeriodConfigs());
            builder.ApplyConfiguration(new UserConfigs());
        }

    }
}
