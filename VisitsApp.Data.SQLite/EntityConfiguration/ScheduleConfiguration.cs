using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitsApp.Core.Models;

namespace VisitsApp.Data.EntityConfiguration
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedules").HasKey(x=>x.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Groups)
                   .WithMany(x => x.Schedules)
                   .UsingEntity(e => e.ToTable("SchedulesGroups"));
        }
    }
}
