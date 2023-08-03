using ENB.Car.Sales.Entities.Collections;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayInsuranceCompany
    {
        public int Id { get; set; }
        public string Insurance_Company_Name { get; set; } = string.Empty;
        public string Other_Details { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public InsurancePolicies? InsurancePolicies { get; set; }
    }
}
