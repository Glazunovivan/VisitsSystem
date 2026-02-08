using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitSchool.Models;

namespace VisitSchool.DataAccessLayer.EntityConfiguration
{
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("Schedules").HasKey(x=>x.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();
        }
    }
}
