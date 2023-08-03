using ENB.Car.Sales.Entities.Collections;
using System.ComponentModel.DataAnnotations;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditCarModel: IValidatableObject
    {
        public int Id { get; set; }
        public string Model_code { get; set; } = string.Empty;
        public string Manufacturer_code { get; set; } = string.Empty;
        public string Model_Name { get; set; } = string.Empty;
        public CarsForSales? CarForSales { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrEmpty(Model_Name)) 
            { 
              yield return new ValidationResult("Model_Name can't be empty", new[] { "Model_Name" });
            }
            if (string.IsNullOrEmpty(Model_code)) 
            {
                yield return new ValidationResult("Model code can't be empty", new[] { "Model_code" });
            }
            if (string.IsNullOrEmpty(Manufacturer_code)) 
            {
                yield return new ValidationResult("Manufacturer code can't be empty", new[] { "Manufacturer_code" });
            }
        }
    }
}
