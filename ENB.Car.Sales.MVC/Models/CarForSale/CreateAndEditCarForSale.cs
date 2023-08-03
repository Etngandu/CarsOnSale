using ENB.Car.Sales.Entities.Collections;
using ENB.Car.Sales.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditCarForSale : IValidatableObject
    {
        public int Id { get; set; }
        public int CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
        public int CarManufacturerId { get; set; }
        public CarManufacturer? CarManufacturer { get; set; }
        public decimal Asking_Price { get; set; }
        public string Current_Mileage { get; set; } = string.Empty;
        public DateTime Date_Acquired { get; set; }
        public DateTime Registration_Year { get; set; }
        public string? Other_details { get; set; }       
        public CarSolds? CarSolds { get; set; }
        public VehiculeCategory VehiculeCategory { get; set; }
        public List<SelectListItem>? ListMafacturers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Asking_Price == 0)
            {
                yield return new ValidationResult("Model_Name can't be empty", new[] { "Model_Name" });
            }
            if (string.IsNullOrEmpty(Current_Mileage))
            {
                yield return new ValidationResult("Current_Mileage code can't be empty", new[] { "Current_Mileage" });
            }
            if (Date_Acquired>DateTime.Today)
            {
                yield return new ValidationResult("Date_Acquired  can't be in the futur", new[] { "Date_Acquired" });
            }
            if (Registration_Year > DateTime.Today)
            {
                yield return new ValidationResult("Registration_Year  can't be in the futur", new[] { "Registration_Year" });
            }
            if (string.IsNullOrEmpty(Other_details))
            {
                yield return new ValidationResult("Other_details  can't be empty", new[] { "Other_details" });
            }
        }
    }
}
