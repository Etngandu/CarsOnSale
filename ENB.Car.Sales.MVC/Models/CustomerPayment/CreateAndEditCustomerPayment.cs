using ENB.Car.Sales.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditCustomerPayment : IValidatableObject
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int? CarSoldId { get; set; }
        public CarSold? CarSold { get; set; }
        public Payment_Status Payment_Status { get; set; }
        public DateTime Customer_Payment_Date_Due { get; set; }
        public DateTime Customer_Payment_Date_Made { get; set; }        
        public decimal Actual_Payment_Amount { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Payment_Status == Payment_Status.None)
            {
                yield return new ValidationResult("Payment_Status can't be None", new[] { "Payment_Status" });
            }
            if (Customer_Payment_Date_Due < DateTime.Today)
            {
                yield return new ValidationResult("Customer_Payment_Date_Due can't be in the past", new[] { "Customer_Payment_Date_Due" });
            }
            if (Customer_Payment_Date_Made < DateTime.Today)
            {
                yield return new ValidationResult("Customer_Payment_Date_Made can't be in the past", new[] { "Customer_Payment_Date_Made" });
            }
            if (Actual_Payment_Amount < 0)
            {
                yield return new ValidationResult("Actual_Payment_Amount can't be empty", new[] { "Actual_Payment_Amount" });
            }
            
        }
    }
}
