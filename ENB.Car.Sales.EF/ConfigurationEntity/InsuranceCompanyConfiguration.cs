
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
    public class InsuranceCompanyConfiguration
    {
        public void Configure(EntityTypeBuilder<InsuranceCompany> builder)
        {

            builder.Property(x => x.Other_Details).IsRequired().HasMaxLength(250);            
            builder.Property(x => x.Insurance_Company_Name).IsRequired().HasMaxLength(150);

        }
    }
}
