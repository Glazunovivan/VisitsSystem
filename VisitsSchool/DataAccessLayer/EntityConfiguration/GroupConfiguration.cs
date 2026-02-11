using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitsApp.Core.Models;

namespace VisitSchool.DataAccessLayer.EntityConfiguration
{
    public class GroupConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Groups").HasKey(x=>x.Id);
            builder.HasMany(x => x.Students).WithOne(x=>x.Group);

            builder.Property(x=>x.Id).ValueGeneratedOnAdd();
        }
    }
}
