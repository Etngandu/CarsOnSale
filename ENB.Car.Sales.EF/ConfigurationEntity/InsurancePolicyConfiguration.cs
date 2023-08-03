
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
    public class InsurancePolicyConfiguration
    {
        public void Configure(EntityTypeBuilder<InsurancePolicy> builder)
        {

            builder.Property(x => x.Other_Details).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Monthtly_payment).IsRequired().HasPrecision(18,2);
            builder.HasOne<InsuranceCompany>(isc => isc.InsuranceCompany)
               .WithMany(isp => isp.InsurancePolicies)
               .HasForeignKey(x => x.InsuranceCompanyId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<CarSold>(cs => cs.CarSold)
                .WithMany(ip => ip.InsurancePolicies)
                .HasForeignKey(x => x.CarSoldId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Customer>(cs => cs.Customer)
                .WithMany(y => y.InsurancePolicies)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
