using ENB.Car.Sales.Entities.Collections;
using System.ComponentModel.DataAnnotations;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditCarManufacturer :IValidatableObject
    {
        public int Id { get; set; }
        public string Manufacturer_Shortname { get; set; } = string.Empty;
        public string Manufacturer_FullName { get; set; } = string.Empty;
        public string Manufacturer_OtherDetails { get; set; } = string.Empty;        

        public CarsForSales? CarForSales { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            if (string.IsNullOrEmpty(Manufacturer_Shortname))
            {
                yield return new ValidationResult("Manufacturer_Shortname code can't be empty", new[] { "Manufacturer_Shortname" });
            }
            if (string.IsNullOrEmpty(Manufacturer_FullName))
            {
                yield return new ValidationResult("Manufacturer_FullName code can't be empty", new[] { "Manufacturer_FullName" });
            }
            if (string.IsNullOrEmpty(Manufacturer_OtherDetails))
            {
                yield return new ValidationResult("Manufacturer_OtherDetails code can't be empty", new[] { "Manufacturer_OtherDetails" });
            }
        }
    }
}
