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
    public class CarModel : DomainEntity<int>, IDateTracking
    {
        public CarModel()
        {
            CarForSales = new();
            FeaturesOnCarsForSales= new();
        }
        public string Model_code { get; set; } = string.Empty;
        public string Manufacturer_code { get; set; } = string.Empty;
        public string Model_Name{ get; set; } = string.Empty;
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }
        public CarsForSales  CarForSales { get; set; }
        public FeaturesOnCarsForSales FeaturesOnCarsForSales  { get; set; }


        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Model_code))
             yield return new ValidationResult("Model_code can't be Empty", new[] { "Model_code"});

            if (string.IsNullOrEmpty(Manufacturer_code))
                yield return new ValidationResult("Manufacturer_code can't be Empty", new[] { "Manufacturer_code" });

            if (string.IsNullOrEmpty(Model_Name))
                yield return new ValidationResult("Model_Name can't be Empty", new[] { "Model_Name" });
        }
    }
}
