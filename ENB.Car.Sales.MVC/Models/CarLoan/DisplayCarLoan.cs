using ENB.Car.Sales.Entities;
using Microsoft.EntityFrameworkCore;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayCarLoan
    {
        public int Id { get; set; }
        public int CarSoldId { get; set; }
        public CarSold? CarSold { get; set; }
        public int FinanceCompanyId { get; set; }
        public FinanceCompany? FinanceCompany { get; set; }
        public int CustomerId { get; set; }
        public Customer?  Customer { get; set; }
        public DateTime Repayment_Start_Date { get; set; }
        public DateTime Repayment_End_Date { get; set; }        
        public decimal Monthtly_Repayment { get; set; }
        public string Other_Details { get; set; } = string.Empty;
        public string NameCustomer { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
