using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ENB.InsuranceAndClaims.Entities
{
    /// <summary>
    /// Determines the type of a contact record.
    /// </summary>
    public enum Ref_Claim_Type
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        [Display(Name = "Burglary and Theft")]
        /// <summary>
        /// Indicates a business contact record.
        /// </summary>
        Burglary_and_Theft = 1,

        [Display(Name = "Water and Freezing Damage")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Water_and_Freezing_Damage = 2,

        [Display(Name = "Wind and Hail Damage")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Wind_and_Hail_Damage = 3,

        [Display(Name = "Fire")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Fire = 4
    }
}
