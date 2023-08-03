using ENB.Car.Sales.Entities.Collections;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayCarModel
    {
        public int Id { get; set; }
        public string Model_code { get; set; } = string.Empty;
        public string Manufacturer_code { get; set; } = string.Empty;
        public string Model_Name { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public CarsForSales? CarForSales { get; set; }
    }
}
