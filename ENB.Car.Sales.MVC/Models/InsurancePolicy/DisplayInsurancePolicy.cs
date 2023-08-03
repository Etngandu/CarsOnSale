using ENB.Car.Sales.Entities;
using Microsoft.EntityFrameworkCore;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayInsurancePolicy
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
        public string NameCustomer { get; set; } = string.Empty;
        public string Other_Details { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
