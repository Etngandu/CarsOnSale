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
    public class CarForSale : DomainEntity<int>, IDateTracking
    {
        public CarForSale()
        {
            CarSolds = new();
            FeaturesOnCarsForSales= new();
        }
        public int CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
        public int CarManufacturerId { get; set; }
        public CarManufacturer? CarManufacturer { get; set; }

        [Precision(18, 2)]
        public decimal Asking_Price { get; set; }
        public string Current_Mileage { get; set; } = string.Empty;
        public DateTime Date_Acquired { get; set; }
        public DateTime Registration_Year { get; set; }
        public string? Other_details { get; set; }
        public VehiculeCategory  VehiculeCategory { get; set; }
        public DateTime DateCreated { get ; set ; }
        public DateTime DateModified { get ; set ; }
        public CarSolds  CarSolds { get; set; }

        public FeaturesOnCarsForSales FeaturesOnCarsForSales  { get; set; }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Asking_Price==0)
                yield return new ValidationResult("Asking_Price can't be Empty", new[] { "Asking_Price" });
        }
    }
}
