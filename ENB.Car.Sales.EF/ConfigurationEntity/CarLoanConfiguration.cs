
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
    public class CarLoanConfiguration
    {
        public void Configure(EntityTypeBuilder<CarLoan> builder)
        {

            builder.Property(x => x.Other_Details).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Monthtly_Repayment).IsRequired().HasPrecision(18,2);
            builder.HasOne<FinanceCompany>(f => f.FinanceCompany)
               .WithMany(cl => cl.CarLoans)
               .HasForeignKey(x => x.FinanceCompanyId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<CarSold>(cs => cs.CarSold)
                .WithMany(cl => cl.CarLoans)
                .HasForeignKey(x => x.CarSoldId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Customer>(cs => cs.Customer)
                .WithMany(y => y.CarLoans)
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
