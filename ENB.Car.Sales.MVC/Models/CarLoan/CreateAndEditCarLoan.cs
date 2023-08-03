using ENB.Car.Sales.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditCarLoan : IValidatableObject
    {
        public int Id { get; set; }
        public int CarSoldId { get; set; }
        public CarSold? CarSold { get; set; }
        public int FinanceCompanyId { get; set; }
        public FinanceCompany? FinanceCompany { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime Repayment_Start_Date { get; set; }
        public DateTime Repayment_End_Date { get; set; }
        public decimal Monthtly_Repayment { get; set; }
        public List<SelectListItem>? ListFinanceCompany { get; set; }
        public string Other_Details { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            if (Repayment_Start_Date < DateTime.Today)
            {
                yield return new ValidationResult("Repayment_Start_Date can't be in the past", new[] { "Repayment_Start_Date" });
            }
            if (Repayment_End_Date < DateTime.Today)
            {
                yield return new ValidationResult("Repayment_End_Date can't be in the past", new[] { "Repayment_End_Date" });
            }
            if (Monthtly_Repayment == 0)
            {
                yield return new ValidationResult("Monthty_Repayment can't be empty", new[] { "Monthty_Repayment" });
            }
            if (string.IsNullOrEmpty(Other_Details))
            {
                yield return new ValidationResult("Other_Details code can't be empty", new[] { "Other_Details" });
            }
        }
    }
}
