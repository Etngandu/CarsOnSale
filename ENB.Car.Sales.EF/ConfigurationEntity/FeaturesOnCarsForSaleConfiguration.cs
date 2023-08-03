
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
    public class FeaturesOnCarsForSaleConfiguration
    {
        public void Configure(EntityTypeBuilder<FeaturesOnCarsForSale> builder)
        {

            builder.HasOne<CarForSale>(cm => cm.CarForSale)
               .WithMany(cl => cl.FeaturesOnCarsForSales)
               .HasForeignKey(x => x.CarForSaleId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<CarFeature>(cs => cs.CarFeature)
                .WithMany(cf => cf.FeaturesOnCarsForSales)
                .HasForeignKey(y => y.CarFeatureId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<CarModel>(cs => cs.CarModel)
                .WithMany(cf => cf.FeaturesOnCarsForSales)
                .HasForeignKey(y => y.CarModelId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
