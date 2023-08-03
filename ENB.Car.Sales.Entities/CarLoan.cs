﻿using ENB.Car.Sales.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Car.Sales.Entities
{
    public class CarLoan : DomainEntity<int>, IDateTracking
    {
        public int CarSoldId { get; set; }
        public CarSold? CarSold { get; set; }
        public int FinanceCompanyId { get; set; }
        public FinanceCompany? FinanceCompany { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer  { get; set; }
        public DateTime Repayment_Start_Date { get; set; }
        public DateTime Repayment_End_Date { get; set; }

        [Precision(18, 2)]
        public  decimal Monthtly_Repayment { get; set; }
        public string Other_Details { get; set; } = string.Empty;

        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
