// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="Diesel.cs">
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
    /// Diesel petrol derived from FuelType
    /// </summary>
    public class Diesel : FuelType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Diesel"/> class.
        /// </summary>
        public Diesel() : base(FuelTypes.Diesel.ToString())
        {
        }
    }
}