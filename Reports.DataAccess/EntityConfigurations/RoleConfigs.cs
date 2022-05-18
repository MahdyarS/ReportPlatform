using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.DataAccess.Entities.Users;
using Reports.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.DataAccess.EntityConfigurations
{
    public class RoleConfigs : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasData(new Role { Name = RoleName.Admin.ToString(), NormalizedName = RoleName.Admin.ToString().ToUpper() });
            builder.HasData(new Role { Name = RoleName.User.ToString(), NormalizedName = RoleName.User.ToString().ToUpper() });

            return;
        }
    }
}
