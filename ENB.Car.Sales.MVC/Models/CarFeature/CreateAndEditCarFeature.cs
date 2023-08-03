using ENB.Car.Sales.Entities.Collections;
using System.ComponentModel.DataAnnotations;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditCarFeature: IValidatableObject
    {
        public int CarFeatureId { get; set; }
        public string Car_Feature_description { get; set; } = string.Empty;
        public CustomerPreferences? CustomerPreferences { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(string.IsNullOrEmpty(Car_Feature_description)) 
            {
                yield return new ValidationResult("Car Feature description can't be empty", new[] { "Car_Feature_description" });
            }
        }
    }
}
