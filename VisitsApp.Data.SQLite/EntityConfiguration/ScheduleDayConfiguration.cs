using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitsApp.Core.Models;

namespace VisitsApp.Data.EntityConfiguration
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
