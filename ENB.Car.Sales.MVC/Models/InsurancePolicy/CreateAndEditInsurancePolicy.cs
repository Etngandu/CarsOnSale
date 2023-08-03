using ENB.Car.Sales.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditInsurancePolicy : IValidatableObject
    {
        public int Id { get; set; }
        public int CarSoldId { get; set; }
        public CarSold? CarSold { get; set; }
        public int InsuranceCompanyId { get; set; }
        public InsuranceCompany? InsuranceCompany { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public DateTime Policy_Start_Date { get; set; }
        public DateTime Policy_Renewal_Date { get; set; }
        public decimal Monthtly_payment { get; set; }
        public List<SelectListItem>? ListInsuranceCompany { get; set; }
        public string Other_Details { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            if (Policy_Start_Date < DateTime.Today)
            {
                yield return new ValidationResult("Policy_Start_Date can't be in the past", new[] { "Policy_Start_Date" });
            }
            if (Policy_Renewal_Date < DateTime.Today)
            {
                yield return new ValidationResult("Policy_Renewal_Date can't be in the past", new[] { "Policy_Renewal_Date" });
            }
            if (Monthtly_payment == 0)
            {
                yield return new ValidationResult("Monthty_Repayment can't be empty", new[] { "Actual_Payment_Amount" });
            }
            if (string.IsNullOrEmpty(Other_Details))
            {
                yield return new ValidationResult("Other_Details code can't be empty", new[] { "Other_Details" });
            }
        }
    }
}
