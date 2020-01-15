// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="Van.cs">
//   Copyright of Ed French @ARU
// </copyright>
// <summary>
//              This class represent a van which inherits from the base class vehicle - so we can add vans to a list of vehicles
// </summary>
// <rationale>
//              I have encapsulated the generation of a random fuel type with in this class as external code does not need to know about it.
// </rationale>
// <optmisation>
//                  the fuel capacity is defined as a constant as it is unchanging
// </optmisation>
//// --------------------------------------------------------------------------------------------------------------------
namespace Assignment_2_PetrolStation_VeryHighLevel
{
    using System;

    /// <summary>
    /// A car class that inherits from the base class Vehicle
    /// </summary>
    public class Van : Vehicle
    {
        /// <summary>
        /// The maximum fuel capacity.
        /// </summary>
        private new const int FuelCapacity = 80000; // millilitres for more accurate calcs

        /// <summary>
        /// Initializes a new instance of the <see cref="Van"/> class. 
        /// Constructor
        /// </summary>
        public Van()
            : base(VehicleTypes.Van.ToString(), FuelCapacity)
        {
        }
    }
}