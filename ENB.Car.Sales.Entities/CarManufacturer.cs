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
    public class CarManufacturer : DomainEntity<int>, IDateTracking
    {
        public CarManufacturer()
        {
            CarForSales = new(); 
        }
        public string Manufacturer_Shortname { get; set; } = string.Empty;
        public string Manufacturer_FullName { get; set; } = string.Empty;
        public string Manufacturer_OtherDetails { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public CarsForSales CarForSales  { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Manufacturer_Shortname))
                yield return new ValidationResult("Manufacturer_Shortname can't be Empty", new[] { "Manufacturer_Shortname" });

            if (string.IsNullOrEmpty(Manufacturer_Shortname))
                yield return new ValidationResult("Manufacturer_Shortname can't be Empty", new[] { "Manufacturer_Shortname" });
        }
    }
}
