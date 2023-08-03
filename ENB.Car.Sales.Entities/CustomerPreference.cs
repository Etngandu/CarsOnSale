using ENB.Car.Sales.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Car.Sales.Entities
{
    public class CustomerPreference : DomainEntity<int>, IDateTracking
    {
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int CarFeatureId { get; set; }
        public CarFeature? CarFeature { get; set; }
        public string Customer_Preference_Details { get; set; } = string.Empty;
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
