using ENB.Car.Sales.Entities.Collections;
using ENB.Car.Sales.Entities;
using Microsoft.EntityFrameworkCore;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayCarForSale
    {
        public int Id { get; set; }
        public int CarModelId { get; set; }
        public CarModel? CarModel { get; set; }
        public int CarManufacturerId { get; set; }
        public CarManufacturer? CarManufacturer { get; set; }        
        public decimal Asking_Price { get; set; }
        public string Current_Mileage { get; set; } = string.Empty;
        public DateTime Date_Acquired { get; set; }
        public DateTime Registration_Year { get; set; }
        public string? Other_details { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public CarSolds? CarSolds { get; set; }
        public VehiculeCategory VehiculeCategory { get; set; }
        public string Model_Name { get; set; } = string.Empty;
        public string Manufacturer_Name { get; set; } = string.Empty;
    }
}
