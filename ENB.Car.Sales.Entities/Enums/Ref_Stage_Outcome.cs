using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ENB.InsuranceAndClaims.Entities
{
    /// <summary>
    /// Determines the type of a contact record.
    /// </summary>
    public enum Ref_Stage_Outcome
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        /// <summary>
        /// Indicates a business contact record.
        /// </summary>
        Disputed = 1,

        [Display(Name = "In Progress")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        In_Progress = 2,

       
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Settled = 3
    }
}
