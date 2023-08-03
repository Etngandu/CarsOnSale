using ENB.Car.Sales.Entities.Collections;
using ENB.Car.Sales.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENB.Car.Sales.Entities
{
    public class InsuranceCompany : DomainEntity<int>, IDateTracking
    {
        public InsuranceCompany()
        {
            InsurancePolicies = new();
        }
        public string Insurance_Company_Name { get; set; } = string.Empty;
        public string Other_Details { get; set; } = string.Empty;
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }
        public InsurancePolicies  InsurancePolicies { get; set; }
        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
