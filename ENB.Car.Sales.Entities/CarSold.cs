using ENB.Car.Sales.Entities.Collections;
using ENB.Car.Sales.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Car.Sales.Entities
{
    public class CarSold : DomainEntity<int>, IDateTracking
    {
        public CarSold()
        {
            InsurancePolicies = new ();
            CarLoans= new ();
            CustomerPayments= new ();
        }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int CarForSaleId { get; set; }
        public CarForSale? CarForSale { get; set; }
        [Precision(18, 2)]
        public decimal Agreed_Price { get; set; }
        public DateTime Date_Sold { get; set; }
        [Precision(18, 2)]
        public decimal Monthly_Payment_Amount { get; set; }
        public string Monthly_Payment_Date { get; set; } = string.Empty;
        public string Other_Details { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public  CustomerPayments  CustomerPayments { get; set; }
        public CarLoans CarLoans  { get; set; }
        public InsurancePolicies  InsurancePolicies { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
