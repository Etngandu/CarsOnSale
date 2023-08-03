
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
    public class CarManufacturerConfiguration
    {
        public void Configure(EntityTypeBuilder<CarManufacturer> builder)
        {

            builder.Property(x => x.Manufacturer_Shortname).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Manufacturer_FullName).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Manufacturer_OtherDetails).IsRequired().HasMaxLength(250);           

        }
    }
}
