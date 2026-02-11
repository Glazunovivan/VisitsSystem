using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitsApp.Core.Models;

namespace VisitSchool.DataAccessLayer.EntityConfiguration
{
    public class VisitConfiguration : IEntityTypeConfiguration<Visit>
    {
        public void Configure(EntityTypeBuilder<Visit> builder)
        {
            builder.ToTable("Visits")
                   .HasKey(x=>x.Id);
            builder.HasOne(x => x.Student);

            builder.HasOne(x=>x.Schedule);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();
        
        }
    }
}
