using ENB.Car.Sales.Entities.Collections;
using System.ComponentModel.DataAnnotations;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditFinanceCompany :IValidatableObject
    {
        public int Id { get; set; }
        public string Finance_Company_Name { get; set; } = string.Empty;
        public string Other_Details { get; set; } = string.Empty;        
        public CarLoans? CarLoans { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Finance_Company_Name))
            {
                yield return new ValidationResult("Finance_Company_Name code can't be empty", new[] { "Finance_Company_Name" });
            }
            if (string.IsNullOrEmpty(Other_Details))
            {
                yield return new ValidationResult("Other_Details code can't be empty", new[] { "Other_Details" });
            }
        }
    }
}
