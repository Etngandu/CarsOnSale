
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
    public class CarForSaleConfiguration
    {
        public void Configure(EntityTypeBuilder<CarForSale> builder)
        {

            builder.Property(x => x.Other_details).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Asking_Price).IsRequired().HasPrecision(18, 4);                
            builder.Property(x => x.Current_Mileage).IsRequired().HasMaxLength(10);
            builder.HasOne<CarModel>(cm => cm.CarModel)
               .WithMany(cl => cl.CarForSales)
               .HasForeignKey(x => x.CarModelId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<CarManufacturer>(cs => cs.CarManufacturer)
                .WithMany(cf => cf.CarForSales)
                .HasForeignKey(y => y.CarManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
