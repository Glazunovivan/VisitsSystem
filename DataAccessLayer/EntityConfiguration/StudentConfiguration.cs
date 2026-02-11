using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitsApp.Core.Models;

namespace VisitSchool.DataAccessLayer.EntityConfiguration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student").Property(p => p.Name).IsRequired();
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).HasColumnName("user_id");
            builder.HasOne(x=>x.Group)
                   .WithMany(x=>x.Students);

            builder.Property(p=>p.Id).ValueGeneratedOnAdd();
        }
    }
}
