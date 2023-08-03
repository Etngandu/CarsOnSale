
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
    public class CarFeatureConfiguration
    {
        public void Configure(EntityTypeBuilder<CarFeature> builder)
        {

            builder.Property(x => x.Car_Feature_description).IsRequired().HasMaxLength(150);           

        }
    }
}
