using ENB.Car.Sales.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditCustomerPreference :IValidatableObject
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int CarFeatureId { get; set; }
        public CarFeature? CarFeature { get; set; }
        public string Customer_Preference_Details { get; set; } = string.Empty;

        public List<SelectListItem>? ListCarFeature { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Customer_Preference_Details))
            {
                yield return new ValidationResult("Customer_Preference_Details code can't be empty", new[] { "Customer_Preference_Details" });
            }
        }
    }
}
