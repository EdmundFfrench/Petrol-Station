// --------------------------------------------------------------------------------------------------------------------
// <copyright company="Ed French" file="Car.cs">
//   Copyright of Ed French @ARU
// </copyright>
// <summary>
//              This class represent a car which inherits from the base class vehicle - so we can add cars to a list of vehicles
// </summary>
// <rationale>
//              
// </rationale>
// <optmisation>
//                  the fuel capacity is defined as a constant as it is unchanging
// </optmisation>
// --------------------------------------------------------------------------------------------------------------------
namespace Assignment_2_PetrolStation_VeryHighLevel
{
    using System;

    /// <summary>
    /// A car class that inherits from the base class Vehicle
    /// </summary>
    public class Car : Vehicle
    {
        /// <summary>
        /// The maximum fuel capacity.
        /// </summary>
        private new const int FuelCapacity = 40000;

        /// <summary>
        /// Initializes a new instance of the <see cref="Car"/> class. 
        /// Constructor
        /// </summary>
        public Car()
            : base(VehicleTypes.Car.ToString(), FuelCapacity)
        {
        }
    }
}
