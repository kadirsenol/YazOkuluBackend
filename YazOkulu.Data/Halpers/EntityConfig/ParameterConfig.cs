using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YazOkulu.Data.Models;

namespace YazOkulu.Data.Halpers.EntityConfig
{
    public class ParameterConfig : IEntityTypeConfiguration<Parameter>
    {
        public void Configure(EntityTypeBuilder<Parameter> builder)
        {
            // Primary Key
            builder.HasKey(p => p.ParameterID);

            // ParameterID otomatik artmasın, manuel değer verilecek
            builder.Property(p => p.ParameterID)
                   .ValueGeneratedNever(); // << manuel id için önemli
        }
    }
}