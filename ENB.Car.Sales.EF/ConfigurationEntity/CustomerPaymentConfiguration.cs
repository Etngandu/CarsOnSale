
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
    public class CustomerPaymentConfiguration
    {
        public void Configure(EntityTypeBuilder<CustomerPayment> builder)
        {

           
            builder.Property(x => x.Actual_Payment_Amount).IsRequired().HasPrecision(18,2);
            builder.HasOne<Customer>(c => c.Customer)
                .WithMany(cs => cs.CustomerPayments)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<CarSold>(cs => cs.CarSold)
                .WithMany(cs => cs.CustomerPayments)
                .HasForeignKey(y => y.CarSoldId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
