using ENB.Car.Sales.Entities;
using System.ComponentModel;

namespace ENB.Car.Sales.MVC.Models
{
    public class DisplayCustomerPreference
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public int CarFeatureId { get; set; }
        public CarFeature? CarFeature { get; set; }
        public string Car_Feature_description { get; set; } = string.Empty;

        [DisplayName("Preference Details")]
        public string Customer_Preference_Details { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
