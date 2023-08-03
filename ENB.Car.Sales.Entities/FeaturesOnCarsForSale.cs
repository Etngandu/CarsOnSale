using ENB.Car.Sales.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Car.Sales.Entities
{
    public class FeaturesOnCarsForSale: DomainEntity<int>, IDateTracking
    {
        public int CarFeatureId { get; set; }
        public CarFeature? CarFeature { get; set; }
        public int CarForSaleId { get; set; }
        public CarForSale? CarForSale { get; set; }
        public int? CarModelId { get; set; }
        public CarModel? CarModel  { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
