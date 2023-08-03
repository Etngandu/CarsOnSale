
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
    public class CustomerPreferenceConfiguration
    {
        public void Configure(EntityTypeBuilder<CustomerPreference> builder)
        {

           
            builder.Property(x => x.Customer_Preference_Details).IsRequired().HasMaxLength(250);
            builder.HasOne<Customer>(c => c.Customer)
                .WithMany(cs => cs.CustomerPreferences)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<CarFeature>(cf => cf.CarFeature)
                .WithMany(cfs => cfs.CustomerPreferences)
                .HasForeignKey(x => x.CarFeatureId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
