using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reports.DataAccess.Entities.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.DataAccess.EntityConfigurations
{
    public class PeriodConfigs : IEntityTypeConfiguration<Period>
    {
        public void Configure(EntityTypeBuilder<Period> builder)
        {
            builder.Property(p => p.StartPeriod).HasColumnType("date");
            builder.Property(p => p.FinishPeriod).HasColumnType("date");
        }
    }
}
