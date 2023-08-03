using ENB.Car.Sales.Entities.Collections;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayCarManufacturer
    {
        public int Id { get; set; }
        public string Manufacturer_Shortname { get; set; } = string.Empty;
        public string Manufacturer_FullName { get; set; } = string.Empty;
        public string Manufacturer_OtherDetails { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public CarsForSales? CarForSales { get; set; }
    }
}
