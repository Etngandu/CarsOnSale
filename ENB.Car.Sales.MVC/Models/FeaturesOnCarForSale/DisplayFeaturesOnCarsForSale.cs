using ENB.Car.Sales.Entities;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayFeaturesOnCarsForSale
    {
        public int Id { get; set; } 
        public int CarFeatureId { get; set; }
        public CarFeature? CarFeature { get; set; }
        public int CarForSaleId { get; set; }
        public CarForSale? CarForSale { get; set; }
        public int CarModelId { get; set; }
        public CarModel? CarModel  { get; set; }
        public string NameCarFeature { get; set; } = string.Empty;
        public string Other_Details { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
