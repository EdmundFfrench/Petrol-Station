// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="Unleaded.cs">
//   Copyright of Ed French @ARU
// </copyright>
// <summary>
//                  
//
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Assignment_2_PetrolStation_VeryHighLevel
{
    /// <summary>
    /// Unleaded petrol derived from FuelType
    /// </summary>
    public class Unleaded : FuelType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Unleaded"/> class.
        /// </summary>
        public Unleaded()
            : base(FuelTypes.Unleaded.ToString())
        {
        }
    }
}