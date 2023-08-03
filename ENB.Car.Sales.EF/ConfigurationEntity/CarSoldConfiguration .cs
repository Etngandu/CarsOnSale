
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
    public class CarSoldConfiguration
    {
        public void Configure(EntityTypeBuilder<CarSold> builder)
        {

            builder.Property(x => x.Other_Details).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Agreed_Price).IsRequired().HasPrecision(18,2);
            builder.Property(x => x.Monthly_Payment_Amount).IsRequired().HasPrecision(18, 2);
            builder.Property(x => x.Monthly_Payment_Date).IsRequired().HasMaxLength(18);
            builder.HasOne<Customer>(c => c.Customer)
               .WithMany(cs => cs.CarSolds)
               .HasForeignKey(x => x.CustomerId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<CarForSale>(cfs => cfs.CarForSale)
               .WithMany(cfss => cfss.CarSolds)
               .HasForeignKey(x => x.CarForSaleId)
               .OnDelete(DeleteBehavior.Restrict);
            

        }
    }
}
