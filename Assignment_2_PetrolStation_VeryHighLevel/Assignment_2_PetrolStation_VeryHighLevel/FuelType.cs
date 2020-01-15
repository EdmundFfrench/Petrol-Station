// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="FuelType.cs">
//   Copyright of Ed French @ARU
// </copyright>
// <summary>
//              I have made this a class because this class could be extended. For example a different price could be added for each fuel type.
//
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Assignment_2_PetrolStation_VeryHighLevel
{
    /// <summary>
    /// The base fuel type class
    /// </summary>
    public class FuelType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FuelType"/> class.
        /// </summary>
        /// <param name="fuelName">
        /// The fuel name.
        /// </param>
        public FuelType(string fuelName)
        {
            this.FuelName = fuelName;
        }

        /// <summary>
        /// Possible fuel types
        /// </summary>
        public enum FuelTypes
        {
            /// <summary>
            /// Unleaded fuel type
            /// </summary>
            Unleaded,

            /// <summary>
            /// Diesel fuel type
            /// </summary>
            Diesel,

            /// <summary>
            /// Lpg fuel type
            /// </summary>
            Lpg
        }

        /// <summary>
        /// Gets the name of the fuel type
        /// </summary>
        public string FuelName { get; }
    }
}