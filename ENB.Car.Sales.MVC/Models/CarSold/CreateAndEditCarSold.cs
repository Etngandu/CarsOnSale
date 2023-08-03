using ENB.Car.Sales.Entities.Collections;
using ENB.Car.Sales.Entities;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditCarSold : IValidatableObject
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int CarForSaleId { get; set; }
        public CarForSale? CarForSale { get; set; }
        public decimal Agreed_Price { get; set; }
        public DateTime Date_Sold { get; set; }
        public decimal Monthly_Payment_Amount { get; set; }
        public string Monthly_Payment_Date { get; set; } = string.Empty;
        public string Other_Details { get; set; } = string.Empty;   
        public CustomerPayments? CustomerPayments { get; set; }
        public List<SelectListItem>? ListCarForSale { get; set; }
        public CarLoans? CarLoans { get; set; }
        public InsurancePolicies? InsurancePolicies { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Agreed_Price==0)
            {
                yield return new ValidationResult("Agreed_Price can't be empty", new[] { "Agreed_Price" });
            }
            if (Date_Sold > DateTime.Today)
            {
                yield return new ValidationResult("Date_Sold can't be in the futur", new[] { "Date_Sold" });
            }
            if (Monthly_Payment_Amount == 0)
            {
                yield return new ValidationResult("Monthly_Payment_Amount can't be empty", new[] { "Monthly_Payment_Amount" });
            }
            if (string.IsNullOrEmpty(Monthly_Payment_Date))
            {
                yield return new ValidationResult("Monthly_Payment_Date code can't be empty", new[] { "Monthly_Payment_Date" });
            }
            if (string.IsNullOrEmpty(Other_Details))
            {
                yield return new ValidationResult("Other_Details code can't be empty", new[] { "Other_Details" });
            }
        }
    }
}
