using ENB.Car.Sales.Entities.Collections;
using ENB.Car.Sales.Entities;
using Microsoft.EntityFrameworkCore;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayCarSold
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
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string NameCustomer { get; set; } = string.Empty;
        public string CarForSaleCode { get; set; } = string.Empty;
        public CustomerPayments? CustomerPayments { get; set; }
        public CarLoans? CarLoans { get; set; }
        public InsurancePolicies? InsurancePolicies { get; set; }
    }
}
