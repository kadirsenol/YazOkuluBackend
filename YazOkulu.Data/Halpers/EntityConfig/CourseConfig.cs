using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Models;

namespace YazOkulu.Data.Halpers.EntityConfig
{
    public class CourseConfig : IEntityTypeConfiguration<Course>
    {
        public virtual void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.Property(p => p.Code).HasMaxLength(20).IsRequired();
        }
    }
}
