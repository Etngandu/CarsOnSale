using ENB.Car.Sales.Entities;
using ENB.Car.Sales.Entities.Collections;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayCustomer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = String.Empty;

        public string LastName { get; set; } = String.Empty;

        public string FullName { get; set; } = string.Empty;

        public string EmailAddress { get; set; } = String.Empty;

        public Gender Gender { get; set; }

        public string PhoneNumber { get; set; } = String.Empty;

        public DateTime DateOfBirth { get; set; }    
        public string Other_details { get; set; } = String.Empty;

       // public Policies? Policies { get; set; }
        public Address? AddressCustomer { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}

