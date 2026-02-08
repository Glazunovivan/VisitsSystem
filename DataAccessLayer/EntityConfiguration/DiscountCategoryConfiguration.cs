using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VisitSchool.Models;

namespace VisitSchool.DataAccessLayer.EntityConfiguration
{
    public class DiscountCategoryConfiguration : IEntityTypeConfiguration<DiscountCategory>
    {
        public void Configure(EntityTypeBuilder<DiscountCategory> builder)
        {
            builder.ToTable("DiscountCategories").HasKey(x=>x.Id);
        }
    }
}
