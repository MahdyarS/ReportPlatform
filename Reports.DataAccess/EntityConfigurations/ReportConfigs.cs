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
    public class ReportConfigs : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.Property(p => p.StartWorkTime).HasColumnType("time(1)");
            builder.Property(p => p.Date).HasColumnType("date");
            builder.HasQueryFilter(p => !p.IsRemoved);

            return;
        }
    }
}
