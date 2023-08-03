
using ENB.Car.Sales.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Car.Sales.EF.ConfigurationEntity
{
    public class CarModeConfiguration
    {
        public void Configure(EntityTypeBuilder<CarModel> builder)
        {

            builder.Property(x => x.Model_Name).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Model_code).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Manufacturer_code).IsRequired().HasMaxLength(150);

        }
    }
}
