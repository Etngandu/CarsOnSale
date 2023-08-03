using ENB.Car.Sales.Entities.Collections;
using System.ComponentModel;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayCarFeature
    {
        public int Id { get; set; }

        [DisplayName("Car Feature description")]
        public string Car_Feature_description { get; set; } = string.Empty;
        public CustomerPreferences? CustomerPreferences { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
