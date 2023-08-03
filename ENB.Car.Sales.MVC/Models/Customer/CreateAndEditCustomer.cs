using ENB.Car.Sales.Entities;
using ENB.Car.Sales.Entities.Collections;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string EmailAddress { get; set; } = String.Empty;

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; } = String.Empty;

        public DateTime DateOfBirth { get; set; }


        public Address? AddressCustomer { get; set; }


        public string Other_details { get; set; } = String.Empty;

      
    }

    public class CreateAndEditCustomerValidator : AbstractValidator<CreateAndEditCustomer>
    {
        public CreateAndEditCustomerValidator()
        {
            RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("FirstName  can't be empty");

            RuleFor(x => x.LastName)
           .NotEmpty().WithMessage("LastName  can't be empty");

            RuleFor(x => x.EmailAddress)            
           .NotEmpty().WithMessage("Mail")
           .EmailAddress();

            RuleFor(x => x.Gender)
           .NotEqual(Gender.None)
           .WithMessage("Gender  can't be None");

            RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("PhoneNumber  can't be empty");   

            RuleFor(x => x.DateOfBirth)
           .LessThan(x=> DateTime.Now)           
           .WithMessage($"DateOfBirth should be less than {DateTime.Now}" );
           
        }
    
    }
}
