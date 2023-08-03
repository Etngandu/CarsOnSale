using ENB.Car.Sales.Entities.Collections;
using ENB.Car.Sales.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Car.Sales.Entities
{
    public class CarFeature : DomainEntity<int>, IDateTracking
    {
        public CarFeature()
        {
            CustomerPreferences = new();
            FeaturesOnCarsForSales= new();
        }
        public string Car_Feature_description { get; set; } = string.Empty;
        public CustomerPreferences CustomerPreferences { get; set; }
        public FeaturesOnCarsForSales FeaturesOnCarsForSales  { get; set; }
        public DateTime DateCreated { get ; set; }
        public DateTime DateModified { get ; set ; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
