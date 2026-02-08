using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitSchool.Models;

namespace VisitSchool.DataAccessLayer.EntityConfiguration
{
    public class ScheduleDayConfiguration : IEntityTypeConfiguration<ScheduleDay>
    {
        public void Configure(EntityTypeBuilder<ScheduleDay> builder)
        {
            builder.ToTable("ScheduleDays").HasNoKey();
            builder.HasOne(x=>x.Schedule).WithMany(x=>x.Days);
        }
    }
}
