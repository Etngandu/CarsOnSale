using System.ComponentModel.DataAnnotations;

namespace ENB.InsuranceAndClaims.Entities
{
    /// <summary>
    /// Determines the type of a contact record.
    /// </summary>
    public enum Ref_Document_Type
    {
        /// <summary>
        /// Indicates an unidentified value.
        /// </summary>
        None = 0,

        [Display(Name = " Assessor-s Report")]
        /// <summary>
        /// Indicates a business contact record.
        /// </summary>
        Assessors_Report = 1,

        [Display(Name = "Medical Report")]
        /// <summary>
        /// Indicates a personal contact record.
        /// </summary>
        Medical_Report = 2
    }
}
