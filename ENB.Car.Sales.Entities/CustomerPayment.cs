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
    public class CustomerPayment : DomainEntity<int>, IDateTracking
    {
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

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
