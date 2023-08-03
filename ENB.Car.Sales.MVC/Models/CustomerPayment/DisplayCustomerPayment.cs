using ENB.Car.Sales.Entities;
using Microsoft.EntityFrameworkCore;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayCustomerPayment
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int? CarSoldId { get; set; }
        public CarSold? CarSold { get; set; }
        public Payment_Status Payment_Status { get; set; }
        public DateTime Customer_Payment_Date_Due { get; set; }
        public DateTime Customer_Payment_Date_Made { get; set; }
        [Precision(18, 2)]
        public decimal Actual_Payment_Amount { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string NameCustomer { get; set; } = string.Empty;
    }
}
