using ENB.Car.Sales.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENB.Car.Sales.MVC.Models
{
    public class CreateAndEditFeaturesOnCarsForSale
    {
        public int Id { get; set; }
        public int CarFeatureId { get; set; }
        public CarFeature? CarFeature { get; set; }
        public int CarForSaleId { get; set; }
        public CarForSale? CarForSale { get; set; }
        public int CarModelId { get; set; }
        public CarModel? CarModel  { get; set; }
        public List<SelectListItem>? ListCarFeatures { get; set; }
    }
}
