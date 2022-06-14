using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.DataAccess.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.DataAccess.EntityConfigurations
{
    public class UserConfigs : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasAlternateKey(p => p.Email);
            builder.HasIndex(p => p.NationalCode).IsUnique();
            builder.HasIndex(p => p.PhoneNumber).IsUnique();
            builder.HasQueryFilter(p => !p.IsRemoved);

            return;
        }
    }
}
