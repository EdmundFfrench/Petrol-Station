// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="Lpg.cs">
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
    /// Liquid petroleum Gas derived from FuelType
    /// </summary>
    public class Lpg : FuelType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Lpg"/> class.
        /// </summary>
        public Lpg() : base(FuelTypes.Lpg.ToString())
        {
        }
    }
}