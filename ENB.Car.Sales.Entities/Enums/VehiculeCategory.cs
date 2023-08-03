using System.ComponentModel.DataAnnotations;

namespace ENB.Car.Sales.Entities
{
    /// <summary>
    /// Determines the type of a contact person.
    /// </summary>
    public enum VehiculeCategory
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a Divorce Lawyer.
        /// </summary>
        Compact = 1,

        [Display(Name = "In Progress")]
        /// <summary>
        /// Indicates Bankruptcy Lawyer.
        /// </summary>
        Convertible = 2,

        
        /// <summary>
        /// Indicates Employment Lawyer.
        /// </summary>
        Berline = 3,
        
    }
}
